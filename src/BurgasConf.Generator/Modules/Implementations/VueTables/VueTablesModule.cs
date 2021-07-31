using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BurgasConf.Generator.Modules.Implementations.VueTables.Templates;
using BurgasConf.Generator.Services;

namespace BurgasConf.Generator.Modules.Implementations.VueTables
{
    public class VueTablesModule : Module
    {
        private readonly string tablesRelativeFolder;

        public VueTablesModule()
            : base("Vue Tables", true)
        {
            this.tablesRelativeFolder = Path.Combine(
                FoldersNames.Vue,
                FoldersNames.BurgasConfKebapCase,
                FoldersNames.Source,
                FoldersNames.VueComponents);
        }

        public override void SetupFiles()
        {
            var modelReader = GetService<IModelReader>();
            var modelsTypes = modelReader.GetExposedModelsTypes();

            foreach (var modelType in modelsTypes)
            {
                AddFile(new ModuleFile
                {
                    Name = $"{modelType.Name}Table.vue",
                    ReferenceId = modelType.FullName,
                    RelativePath = Path.Combine(SourceDirectory, this.tablesRelativeFolder, FoldersNames.VueTables),
                    RenderFunction = RenderTable,
                    TemplateType = typeof(VueTableTemplate)
                });
            }
        }

        public override void SetupFolders()
        {
            AddFolder(new ModuleFolder
            {
                Name = FoldersNames.VueTables,
                RelativePath = Path.Combine(SourceDirectory, this.tablesRelativeFolder)
            });
        }

        private string RenderTable(ModuleFile moduleFile)
        {
            var modelReader = GetService<IModelReader>();
            var modelType = modelReader
                .GetExposedModelsTypes()
                .FirstOrDefault(x => x.FullName == moduleFile.ReferenceId);

            var publicProperties = modelType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead)
                .Select(x => x.Name)
                .ToList();

            var sessionDictionary = new Dictionary<string, object>
            {
                { "Properties", publicProperties }
            };

            return TemplateRenderer.RenderTemplate(moduleFile.TemplateType, sessionDictionary);
        }
    }
}