using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class HotUpdateMetadataReferenceFeatureProvider : IApplicationFeatureProvider<MetadataReferenceFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, MetadataReferenceFeature feature)
        {
            if (parts == null)
            {
                throw new ArgumentNullException("parts");
            }
            if (feature == null)
            {
                throw new ArgumentNullException("feature");
            }
            HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (ICompilationReferencesProvider compilationReferencesProvider in parts.OfType<ICompilationReferencesProvider>())
            {
                foreach (string text in compilationReferencesProvider.GetReferencePaths())
                {
                    var newText = text;
                    if (string.IsNullOrEmpty(newText))
                    {
                        var assemblyPart = compilationReferencesProvider as AssemblyPart;
                        newText = Path.Combine(AppContext.BaseDirectory, $"{assemblyPart.Name}.dll");
                    }
                    
                    if (hashSet.Add(newText))
                    {
                        feature.MetadataReferences.Add(CreateMetadataReference(newText));
                    }
                }
            }
        }

        private static MetadataReference CreateMetadataReference(string path)
        {
            var stream = File.OpenRead(path);
            var moduleMetadata = ModuleMetadata.CreateFromStream(stream, PEStreamOptions.PrefetchMetadata);
            var assemblyMetadata = AssemblyMetadata.Create(moduleMetadata);

            return assemblyMetadata.GetReference(filePath: path);
        
    }

    }
}
