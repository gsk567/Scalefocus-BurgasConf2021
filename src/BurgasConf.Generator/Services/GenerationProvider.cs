using System;
using System.Collections.Generic;
using BurgasConf.Generator.Models;
using BurgasConf.Generator.Modules;
using BurgasConf.Generator.Modules.Implementations.VueServices;
using BurgasConf.Generator.Modules.Implementations.VueTables;
using BurgasConf.Generator.Modules.Implementations.XamarinModels;
using BurgasConf.Generator.Modules.Implementations.XamarinServices;
using BurgasConf.Generator.Options;
using Microsoft.Extensions.Options;

namespace BurgasConf.Generator.Services
{
    public class GenerationProvider : IGenerationProvider
    {
        private readonly GeneratorOptions options;
        private readonly IEnumerable<Module> modules;
        private readonly IServiceProvider serviceProvider;

        public GenerationProvider(
            IServiceProvider serviceProvider,
            IOptions<GeneratorOptions> optionsAccessor)
        {
            this.options = optionsAccessor.Value;
            this.serviceProvider = serviceProvider;
            this.modules = new List<Module>
            {
                new VueServicesModule(),
                new VueTablesModule(),
                new XamarinModelsModule(),
                new XamarinServicesModule()
            };
        }

        public GenerationResult Generate()
        {
            try
            {
                PrepareModules();
                foreach (var module in this.modules)
                {
                    module.Generate();
                }

                return new GenerationResult
                {
                    Succeeded = true,
                    Message = "Generation completed."
                };
            }
            catch (Exception e)
            {
                return new GenerationResult
                {
                    Succeeded = false,
                    Message = e.Message + " " + e.StackTrace
                };
            }
        }

        private void PrepareModules()
        {
            foreach (var module in this.modules)
            {
                module.SetServiceProvider(this.serviceProvider);
                module.SourceDirectory = this.options.SourceDirectory;
                module.SetupFolders();
                module.SetupFiles();
            }
        }
    }
}