using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Aaron.HotUpdate
{
    /// <summary>
    /// Caches the result of runtime compilation of Razor files for the duration of the application lifetime.
    /// </summary>
    // Token: 0x02000038 RID: 56
    public class HotUpdateRazorViewCompiler : IViewCompiler
    {
        // Token: 0x0600017C RID: 380 RVA: 0x00006D40 File Offset: 0x00004F40
        public HotUpdateRazorViewCompiler(IFileProvider fileProvider, RazorProjectEngine projectEngine, CSharpCompiler csharpCompiler, Action<RoslynCompilationContext> compilationCallback, IList<CompiledViewDescriptor> precompiledViews, ILogger logger)
        {
            if (fileProvider == null)
            {
                throw new ArgumentNullException("fileProvider");
            }
            if (projectEngine == null)
            {
                throw new ArgumentNullException("projectEngine");
            }
            if (csharpCompiler == null)
            {
                throw new ArgumentNullException("csharpCompiler");
            }
            if (compilationCallback == null)
            {
                throw new ArgumentNullException("compilationCallback");
            }
            if (precompiledViews == null)
            {
                throw new ArgumentNullException("precompiledViews");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            this._fileProvider = fileProvider;
            this._projectEngine = projectEngine;
            this._csharpCompiler = csharpCompiler;
            this._compilationCallback = compilationCallback;
            this._logger = logger;
            this._normalizedPathCache = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);
            this._cache = new MemoryCache(new MemoryCacheOptions());
            this._precompiledViews = new Dictionary<string, CompiledViewDescriptor>(precompiledViews.Count, StringComparer.OrdinalIgnoreCase);
            this._cacheKeyList = new List<object>();
            foreach (CompiledViewDescriptor compiledViewDescriptor in precompiledViews)
            {
                logger.ViewCompilerLocatedCompiledView(compiledViewDescriptor.RelativePath);
                if (!this._precompiledViews.ContainsKey(compiledViewDescriptor.RelativePath))
                {
                    this._precompiledViews.Add(compiledViewDescriptor.RelativePath, compiledViewDescriptor);
                }
            }
            if (this._precompiledViews.Count == 0)
            {
                logger.ViewCompilerNoCompiledViewsFound();
            }
        }

        /// <inheritdoc />
        // Token: 0x0600017D RID: 381 RVA: 0x00006E8C File Offset: 0x0000508C
        public Task<CompiledViewDescriptor> CompileAsync(string relativePath)
        {
            if (relativePath == null)
            {
                throw new ArgumentNullException("relativePath");
            }
            Task<CompiledViewDescriptor> result;
            if (CacheExtensions.TryGetValue<Task<CompiledViewDescriptor>>(this._cache, relativePath, out result))
            {
                return result;
            }
            string normalizedPath = this.GetNormalizedPath(relativePath);
            if (CacheExtensions.TryGetValue<Task<CompiledViewDescriptor>>(this._cache, normalizedPath, out result))
            {
                return result;
            }
            result = this.OnCacheMiss(normalizedPath);
            return result;
        }

        // Token: 0x0600017E RID: 382 RVA: 0x00006EDC File Offset: 0x000050DC
        private Task<CompiledViewDescriptor> OnCacheMiss(string normalizedPath)
        {
            object cacheLock = this._cacheLock;
            ViewCompilerWorkItem viewCompilerWorkItem;
            MemoryCacheEntryOptions memoryCacheEntryOptions;
            TaskCompletionSource<CompiledViewDescriptor> taskCompletionSource;
            lock (cacheLock)
            {
                Task<CompiledViewDescriptor> result;
                if (CacheExtensions.TryGetValue<Task<CompiledViewDescriptor>>(this._cache, normalizedPath, out result))
                {
                    return result;
                }
                CompiledViewDescriptor precompiledView;
                if (this._precompiledViews.TryGetValue(normalizedPath, out precompiledView))
                {
                    this._logger.ViewCompilerLocatedCompiledViewForPath(normalizedPath);
                    viewCompilerWorkItem = this.CreatePrecompiledWorkItem(normalizedPath, precompiledView);
                }
                else
                {
                    viewCompilerWorkItem = this.CreateRuntimeCompilationWorkItem(normalizedPath);
                }
                memoryCacheEntryOptions = new MemoryCacheEntryOptions();
                for (int i = 0; i < viewCompilerWorkItem.ExpirationTokens.Count; i++)
                {
                    memoryCacheEntryOptions.ExpirationTokens.Add(viewCompilerWorkItem.ExpirationTokens[i]);
                }
                taskCompletionSource = new TaskCompletionSource<CompiledViewDescriptor>();
                if (!viewCompilerWorkItem.SupportsCompilation)
                {
                    taskCompletionSource.SetResult(viewCompilerWorkItem.Descriptor);
                }
                _cacheKeyList.Add(normalizedPath);
                CacheExtensions.Set<Task<CompiledViewDescriptor>>(this._cache, normalizedPath, taskCompletionSource.Task, memoryCacheEntryOptions);
            }
            if (viewCompilerWorkItem.SupportsCompilation)
            {
                CompiledViewDescriptor descriptor = viewCompilerWorkItem.Descriptor;
                if (((descriptor != null) ? descriptor.Item : null) != null && ChecksumValidator.IsItemValid(this._projectEngine.FileSystem, viewCompilerWorkItem.Descriptor.Item))
                {
                    taskCompletionSource.SetResult(viewCompilerWorkItem.Descriptor);
                    return taskCompletionSource.Task;
                }
                this._logger.ViewCompilerInvalidingCompiledFile(viewCompilerWorkItem.NormalizedPath);
                try
                {
                    CompiledViewDescriptor compiledViewDescriptor = this.CompileAndEmit(normalizedPath);
                    compiledViewDescriptor.ExpirationTokens = memoryCacheEntryOptions.ExpirationTokens;
                    taskCompletionSource.SetResult(compiledViewDescriptor);
                }
                catch (Exception exception)
                {
                    taskCompletionSource.SetException(exception);
                }
            }
            return taskCompletionSource.Task;
        }

        // Token: 0x0600017F RID: 383 RVA: 0x00007068 File Offset: 0x00005268
        private ViewCompilerWorkItem CreatePrecompiledWorkItem(string normalizedPath, CompiledViewDescriptor precompiledView)
        {
            if (precompiledView.Item == null || !ChecksumValidator.IsRecompilationSupported(precompiledView.Item))
            {
                return new ViewCompilerWorkItem
                {
                    SupportsCompilation = false,
                    ExpirationTokens = Array.Empty<IChangeToken>(),
                    Descriptor = precompiledView
                };
            }
            ViewCompilerWorkItem viewCompilerWorkItem = new ViewCompilerWorkItem
            {
                SupportsCompilation = true,
                Descriptor = precompiledView,
                NormalizedPath = normalizedPath,
                ExpirationTokens = new List<IChangeToken>()
            };
            IReadOnlyList<IRazorSourceChecksumMetadata> checksumMetadata = RazorCompiledItemExtensions.GetChecksumMetadata(precompiledView.Item);
            for (int i = 0; i < checksumMetadata.Count; i++)
            {
                viewCompilerWorkItem.ExpirationTokens.Add(this._fileProvider.Watch(checksumMetadata[i].Identifier));
            }
            viewCompilerWorkItem.Descriptor = new CompiledViewDescriptor
            {
                ExpirationTokens = viewCompilerWorkItem.ExpirationTokens,
                IsPrecompiled = true,
                Item = precompiledView.Item,
                RelativePath = precompiledView.RelativePath,
                ViewAttribute = precompiledView.ViewAttribute
            };
            return viewCompilerWorkItem;
        }

        // Token: 0x06000180 RID: 384 RVA: 0x00007154 File Offset: 0x00005354
        private ViewCompilerWorkItem CreateRuntimeCompilationWorkItem(string normalizedPath)
        {
            List<IChangeToken> list = new List<IChangeToken>
            {
                this._fileProvider.Watch(normalizedPath)
            };
            RazorProjectItem item = this._projectEngine.FileSystem.GetItem(normalizedPath);
            if (!item.Exists)
            {
                this._logger.ViewCompilerCouldNotFindFileAtPath(normalizedPath);
                return new ViewCompilerWorkItem
                {
                    SupportsCompilation = false,
                    Descriptor = new CompiledViewDescriptor
                    {
                        RelativePath = normalizedPath,
                        ExpirationTokens = list
                    },
                    ExpirationTokens = list
                };
            }
            this._logger.ViewCompilerFoundFileToCompile(normalizedPath);
            IImportProjectFeature importProjectFeature = this._projectEngine.ProjectFeatures.OfType<IImportProjectFeature>().FirstOrDefault<IImportProjectFeature>();
            IEnumerable<RazorProjectItem> enumerable = (importProjectFeature != null) ? importProjectFeature.GetImports(item) : null;
            foreach (RazorProjectItem razorProjectItem in from import in enumerable ?? Enumerable.Empty<RazorProjectItem>()
                                                          where import.FilePath != null
                                                          select import)
            {
                list.Add(this._fileProvider.Watch(razorProjectItem.FilePath));
            }
            return new ViewCompilerWorkItem
            {
                SupportsCompilation = true,
                NormalizedPath = normalizedPath,
                ExpirationTokens = list
            };
        }

        // Token: 0x06000181 RID: 385 RVA: 0x00007290 File Offset: 0x00005490
        protected virtual CompiledViewDescriptor CompileAndEmit(string relativePath)
        {
            RazorProjectItem item = this._projectEngine.FileSystem.GetItem(relativePath);
            RazorCodeDocument razorCodeDocument = this._projectEngine.Process(item);
            RazorCSharpDocument csharpDocument = RazorCodeDocumentExtensions.GetCSharpDocument(razorCodeDocument);
            if (csharpDocument.Diagnostics.Count > 0)
            {
                //throw CompilationFailedExceptionFactory.Create(razorCodeDocument, csharpDocument.Diagnostics);
                throw new ApplicationException("csharpDocument.Diagnostics.Count > 0");
            }
            Assembly assembly = this.CompileAndEmit(razorCodeDocument, csharpDocument.GeneratedCode);
            RazorCompiledItem item2 = new RazorCompiledItemLoader().LoadItems(assembly).SingleOrDefault<RazorCompiledItem>();
            RazorViewAttribute customAttribute = assembly.GetCustomAttribute<RazorViewAttribute>();
            return new CompiledViewDescriptor(item2, customAttribute);
        }

        // Token: 0x06000182 RID: 386 RVA: 0x0000730C File Offset: 0x0000550C
        internal Assembly CompileAndEmit(RazorCodeDocument codeDocument, string generatedCode)
        {
            this._logger.GeneratedCodeToAssemblyCompilationStart(codeDocument.Source.FilePath);
            long startTimestamp = this._logger.IsEnabled(LogLevel.Debug) ? Stopwatch.GetTimestamp() : 0L;
            string randomFileName = Path.GetRandomFileName();
            CSharpCompilation csharpCompilation = this.CreateCompilation(generatedCode, randomFileName);
            EmitOptions emitOptions = this._csharpCompiler.EmitOptions;
            bool flag = this._csharpCompiler.EmitPdb && (int)emitOptions.DebugInformationFormat != 3;
            Assembly result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (MemoryStream memoryStream2 = flag ? new MemoryStream() : null)
                {
                    EmitResult emitResult = csharpCompilation.Emit(memoryStream, memoryStream2, null, null, null, emitOptions, null, null, null, null, default(CancellationToken));
                    if (!emitResult.Success)
                    {
                        //throw CompilationFailedExceptionFactory.Create(codeDocument, generatedCode, randomFileName, emitResult.Diagnostics);
                        throw new ApplicationException("emitResult fail");
                    }
                    memoryStream.Seek(0L, SeekOrigin.Begin);
                    if (memoryStream2 != null)
                    {
                        memoryStream2.Seek(0L, SeekOrigin.Begin);
                    }
                    Assembly assembly = Assembly.Load(memoryStream.ToArray(), (memoryStream2 != null) ? memoryStream2.ToArray() : null);
                    this._logger.GeneratedCodeToAssemblyCompilationEnd(codeDocument.Source.FilePath, startTimestamp);
                    result = assembly;
                }
            }
            return result;
        }

        // Token: 0x06000183 RID: 387 RVA: 0x00007458 File Offset: 0x00005658
        private CSharpCompilation CreateCompilation(string compilationContent, string assemblyName)
        {
            SourceText sourceText = SourceText.From(compilationContent, Encoding.UTF8, SourceHashAlgorithm.Sha1);
            SyntaxTree syntaxTree = this._csharpCompiler.CreateSyntaxTree(sourceText).WithFilePath(assemblyName);
            RoslynCompilationContext roslynCompilationContext = new RoslynCompilationContext(ExpressionRewriter.Rewrite(this._csharpCompiler.CreateCompilation(assemblyName).AddSyntaxTrees(new SyntaxTree[]
            {
                syntaxTree
            })));
            this._compilationCallback(roslynCompilationContext);
            return roslynCompilationContext.Compilation;
        }

        // Token: 0x06000184 RID: 388 RVA: 0x000074C0 File Offset: 0x000056C0
        private string GetNormalizedPath(string relativePath)
        {
            if (relativePath.Length == 0)
            {
                return relativePath;
            }
            string text;
            if (!this._normalizedPathCache.TryGetValue(relativePath, out text))
            {
                text = ViewPath.NormalizePath(relativePath);
                this._normalizedPathCache[relativePath] = text;
            }
            return text;
        }

        // Token: 0x040000A0 RID: 160
        private readonly object _cacheLock = new object();

        // Token: 0x040000A1 RID: 161
        private readonly Dictionary<string, CompiledViewDescriptor> _precompiledViews;

        // Token: 0x040000A2 RID: 162
        private readonly ConcurrentDictionary<string, string> _normalizedPathCache;

        // Token: 0x040000A3 RID: 163
        private readonly IFileProvider _fileProvider;

        // Token: 0x040000A4 RID: 164
        private readonly RazorProjectEngine _projectEngine;

        // Token: 0x040000A5 RID: 165
        private readonly Action<RoslynCompilationContext> _compilationCallback;

        // Token: 0x040000A6 RID: 166
        private readonly ILogger _logger;

        // Token: 0x040000A7 RID: 167
        private readonly CSharpCompiler _csharpCompiler;

        // Token: 0x040000A8 RID: 168
        private readonly IMemoryCache _cache;

        /// <summary>
        /// cache key
        /// </summary>
        private readonly IList<object> _cacheKeyList;

        /// <summary>
        /// clear view cache
        /// </summary>
        public void ClearViewCache()
        {
            foreach (var item in _cacheKeyList)
                _cache.Remove(item);

            if (_cacheKeyList.Count > 0)
                _cacheKeyList.Clear();
        }

        // Token: 0x02000079 RID: 121
        private class ViewCompilerWorkItem
        {
            // Token: 0x170000B2 RID: 178
            // (get) Token: 0x0600027E RID: 638 RVA: 0x0000A940 File Offset: 0x00008B40
            // (set) Token: 0x0600027F RID: 639 RVA: 0x0000A948 File Offset: 0x00008B48
            public bool SupportsCompilation { get; set; }

            // Token: 0x170000B3 RID: 179
            // (get) Token: 0x06000280 RID: 640 RVA: 0x0000A951 File Offset: 0x00008B51
            // (set) Token: 0x06000281 RID: 641 RVA: 0x0000A959 File Offset: 0x00008B59
            public string NormalizedPath { get; set; }

            // Token: 0x170000B4 RID: 180
            // (get) Token: 0x06000282 RID: 642 RVA: 0x0000A962 File Offset: 0x00008B62
            // (set) Token: 0x06000283 RID: 643 RVA: 0x0000A96A File Offset: 0x00008B6A
            public IList<IChangeToken> ExpirationTokens { get; set; }

            // Token: 0x170000B5 RID: 181
            // (get) Token: 0x06000284 RID: 644 RVA: 0x0000A973 File Offset: 0x00008B73
            // (set) Token: 0x06000285 RID: 645 RVA: 0x0000A97B File Offset: 0x00008B7B
            public CompiledViewDescriptor Descriptor { get; set; }
        }
    }
}
