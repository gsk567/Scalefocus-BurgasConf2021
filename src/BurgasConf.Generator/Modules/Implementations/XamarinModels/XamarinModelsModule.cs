using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BurgasConf.Generator.Modules.Implementations.XamarinModels.Templates;
using BurgasConf.Generator.Services;

namespace BurgasConf.Generator.Modules.Implementations.XamarinModels
{
    public class XamarinModelsModule : Module
    {
        private readonly string modelsRelativeFolder;

        public XamarinModelsModule()
            : base("Xamarin Models", true)
        {
            this.modelsRelativeFolder = Path.Combine(
                    FoldersNames.Xamarin,
                    FoldersNames.BurgasConfSdk
                );
        }

        public override void SetupFiles()
        {
            var modelReader = GetService<IModelReader>();
            var modelsTypes = modelReader.GetExposedModelsTypes();

            foreach (var modelType in modelsTypes)
            {
                AddFile(new ModuleFile
                {
                    Name = $"{modelType.Name}BindableModel.cs",
                    ReferenceId = modelType.FullName,
                    RelativePath = Path.Combine(SourceDirectory, this.modelsRelativeFolder, FoldersNames.XamarinModels),
                    RenderFunction = RenderModel,
                    TemplateType = typeof(XamarinModelTemplate)
                });
            }
        }

        public override void SetupFolders()
        {
            AddFolder(new ModuleFolder
            {
                Name = FoldersNames.XamarinModels,
                RelativePath = Path.Combine(SourceDirectory, this.modelsRelativeFolder)
            });
        }

        private string RenderModel(ModuleFile moduleFile)
        {
            var modelReader = GetService<IModelReader>();
            var modelType = modelReader
                .GetExposedModelsTypes()
                .FirstOrDefault(x => x.FullName == moduleFile.ReferenceId);

            var publicProperties = modelType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead)
                .ToList();

            var sessionDictionary = new Dictionary<string, object>
            {
                { "ModelName", moduleFile.Name.Replace(".cs", string.Empty) },
                { "Properties", publicProperties }
            };

            return TemplateRenderer.RenderTemplate(moduleFile.TemplateType, sessionDictionary);
        }
    }
}