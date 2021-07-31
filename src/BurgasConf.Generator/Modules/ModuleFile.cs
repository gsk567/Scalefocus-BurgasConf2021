using System;
using System.Collections.Generic;

namespace BurgasConf.Generator.Modules
{
    public class ModuleFile
    {
        public string Name { get; set; }

        public string RelativePath { get; set; }

        public Type TemplateType { get; set; }

        public string ReferenceId { get; set; }

        public Func<ModuleFile, string> RenderFunction { get; set; }

        public string RenderTemplate(Dictionary<string, object> sessionDictionary)
        {
            return TemplateRenderer.RenderTemplate(this.TemplateType, sessionDictionary);
        }

        public string RenderTemplate()
        {
            return TemplateRenderer.RenderTemplate(this.TemplateType, new Dictionary<string, object>());
        }
    }
}