using System;
using BurgasConf.Generator.Options;
using BurgasConf.Generator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BurgasConf.Generator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRuntimeGenerators(this IServiceCollection services, Action<GeneratorOptions> generatorOptionsAction)
        {
            if (generatorOptionsAction == null)
            {
                throw new ArgumentNullException(nameof(generatorOptionsAction));
            }

            services.Configure(generatorOptionsAction);

            services.AddScoped<IGenerationProvider, GenerationProvider>();
            services.AddScoped<IControllerReader, ControllerReader>();
            services.AddScoped<IModelReader, ModelReader>();
        }
    }
}