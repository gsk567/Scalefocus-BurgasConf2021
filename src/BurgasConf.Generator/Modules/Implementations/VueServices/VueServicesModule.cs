using System.Collections.Generic;
using System.IO;
using System.Linq;
using BurgasConf.Generator.Modules.Implementations.VueServices.Templates;
using BurgasConf.Generator.Services;

namespace BurgasConf.Generator.Modules.Implementations.VueServices
{
    public class VueServicesModule : Module
    {
        private readonly string servicesRelativeFolder;

        public VueServicesModule()
            : base("Vue Services", true)
        {
            this.servicesRelativeFolder = Path.Combine(
                FoldersNames.Vue,
                FoldersNames.BurgasConfKebapCase,
                FoldersNames.Source);
        }

        public override void SetupFiles()
        {
            var controllerReader = GetService<IControllerReader>();
            var actions = controllerReader.GetExposedActions();
            var uniqueControllers = actions
                .Select(x => x.ControllerName)
                .Distinct();

            foreach (var controller in uniqueControllers)
            {
                AddFile(new ModuleFile
                {
                    Name = $"{controller.Replace("Controller", "Service")}.js",
                    RelativePath = Path.Combine(
                        SourceDirectory,
                        this.servicesRelativeFolder,
                        FoldersNames.VueServices),
                    RenderFunction = RenderService,
                    ReferenceId = controller,
                    TemplateType = typeof(VueServiceTemplate)
                });
            }
        }

        public override void SetupFolders()
        {
            AddFolder(new ModuleFolder
            {
                Name = FoldersNames.VueServices,
                RelativePath = Path.Combine(SourceDirectory, this.servicesRelativeFolder)
            });
        }

        private string RenderService(ModuleFile moduleFile)
        {
            var controllerReader = GetService<IControllerReader>();
            var actions = controllerReader
                .GetExposedActions()
                .Where(x => x.ControllerName == moduleFile.ReferenceId)
                .ToList();

            var sessionDictionary = new Dictionary<string, object>
            {
                { "Actions", actions }
            };

            return TemplateRenderer.RenderTemplate(moduleFile.TemplateType, sessionDictionary);
        }
    }
}