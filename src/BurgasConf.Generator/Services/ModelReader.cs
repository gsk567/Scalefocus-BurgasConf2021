using System;
using System.Collections.Generic;
using System.Linq;
using BurgasConf.Generator.Models;
using BurgasConf.Generator.Options;
using Microsoft.Extensions.Options;

namespace BurgasConf.Generator.Services
{
    public class ModelReader : IModelReader
    {
        private readonly GeneratorOptions generatorOptions;

        public ModelReader(IOptions<GeneratorOptions> generatorOptionsAccessor)
        {
            this.generatorOptions = generatorOptionsAccessor.Value;
        }

        public IEnumerable<Type> GetExposedModelsTypes()
        {
            var result = new List<Type>();

            try
            {
                if (this.generatorOptions.SourceAssemblies == null || this.generatorOptions.SourceAssemblies.Count <= 0)
                {
                    return result;
                }

                foreach (var assembly in this.generatorOptions.SourceAssemblies)
                {
                    result.AddRange(assembly
                        .GetTypes()
                        .Where(x => x.BaseType != null &&
                                    x.GetInterfaces().Any(y => y == typeof(IModel))
                        )
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }
    }
}
