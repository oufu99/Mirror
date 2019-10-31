using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aaron.HotUpdate
{
    // Token: 0x02000039 RID: 57
    public class HotUpdateRazorViewCompilerProvider : IViewCompilerProvider
    {
        // Token: 0x06000185 RID: 389 RVA: 0x000074FC File Offset: 0x000056FC
        public HotUpdateRazorViewCompilerProvider(ApplicationPartManager applicationPartManager, RazorProjectEngine razorProjectEngine, IRazorViewEngineFileProviderAccessor fileProviderAccessor, CSharpCompiler csharpCompiler, IOptions<RazorViewEngineOptions> viewEngineOptionsAccessor, ILoggerFactory loggerFactory)
        {
            this._applicationPartManager = applicationPartManager;
            this._razorProjectEngine = razorProjectEngine;
            this._fileProviderAccessor = fileProviderAccessor;
            this._csharpCompiler = csharpCompiler;
            this._viewEngineOptions = viewEngineOptionsAccessor.Value;
            this._logger = LoggerFactoryExtensions.CreateLogger<RazorViewCompiler>(loggerFactory);
            this._createCompiler = new Func<IViewCompiler>(this.CreateCompiler);
        }

        // Token: 0x06000186 RID: 390 RVA: 0x00007564 File Offset: 0x00005764
        public IViewCompiler GetCompiler()
        {
            if (this._fileProviderAccessor.FileProvider is NullFileProvider)
            {
                throw new InvalidOperationException("NullFileProvider");
            }
            return LazyInitializer.EnsureInitialized<IViewCompiler>(ref this._compiler, ref this._initialized, ref this._initializeLock, this._createCompiler);
        }

        // Token: 0x06000187 RID: 391 RVA: 0x000075D0 File Offset: 0x000057D0
        private IViewCompiler CreateCompiler()
        {
            ViewsFeature viewsFeature = new ViewsFeature();
            this._applicationPartManager.PopulateFeature<ViewsFeature>(viewsFeature);
            return new HotUpdateRazorViewCompiler(this._fileProviderAccessor.FileProvider, this._razorProjectEngine, this._csharpCompiler, this._viewEngineOptions.CompilationCallback, viewsFeature.ViewDescriptors, this._logger);
        }

        // Token: 0x040000A9 RID: 169
        private readonly RazorProjectEngine _razorProjectEngine;

        // Token: 0x040000AA RID: 170
        private readonly ApplicationPartManager _applicationPartManager;

        // Token: 0x040000AB RID: 171
        private readonly IRazorViewEngineFileProviderAccessor _fileProviderAccessor;

        // Token: 0x040000AC RID: 172
        private readonly CSharpCompiler _csharpCompiler;

        // Token: 0x040000AD RID: 173
        private readonly RazorViewEngineOptions _viewEngineOptions;

        // Token: 0x040000AE RID: 174
        private readonly ILogger<RazorViewCompiler> _logger;

        // Token: 0x040000AF RID: 175
        private readonly Func<IViewCompiler> _createCompiler;

        // Token: 0x040000B0 RID: 176
        private object _initializeLock = new object();

        // Token: 0x040000B1 RID: 177
        private bool _initialized;

        // Token: 0x040000B2 RID: 178
        private IViewCompiler _compiler;
    }
}
