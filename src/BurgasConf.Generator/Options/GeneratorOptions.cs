using System.Collections.Generic;
using System.Reflection;

namespace BurgasConf.Generator.Options
{
    public class GeneratorOptions
    {
        public GeneratorOptions()
        {
            SourceAssemblies = new List<Assembly>();
        }

        public IList<Assembly> SourceAssemblies { get; set; }

        public string SourceDirectory { get; set; }
    }
}