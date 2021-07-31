using System.Collections.Generic;
using System.IO;
using System.Linq;
using BurgasConf.Generator.Modules.Implementations.XamarinServices.Templates;
using BurgasConf.Generator.Services;

namespace BurgasConf.Generator.Modules.Implementations.XamarinServices
{
    public class XamarinServicesModule : Module
    {
        private readonly string servicesRelativeFolder;

        public XamarinServicesModule()
            : base("Xamarin Services", true)
        {
            this.servicesRelativeFolder = Path.Combine(
                FoldersNames.Xamarin,
                FoldersNames.BurgasConfSdk
            );
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
                    Name = $"{controller.Replace("Controller", "Service")}.cs",
                    RelativePath = Path.Combine(
                        SourceDirectory,
                        this.servicesRelativeFolder,
                        FoldersNames.XamarinServices),
                    RenderFunction = RenderService,
                    ReferenceId = controller,
                    TemplateType = typeof(XamarinServiceTemplate)
                });
            }
        }

        public override void SetupFolders()
        {
            AddFolder(new ModuleFolder
            {
                Name = FoldersNames.XamarinServices,
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
                { "ServiceName", moduleFile.Name.Replace(".cs", string.Empty) },
                { "Actions", actions }
            };

            return TemplateRenderer.RenderTemplate(moduleFile.TemplateType, sessionDictionary);
        }
    }
}