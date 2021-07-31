using System;
using System.Collections.Generic;

namespace BurgasConf.Generator.Services
{
    public interface IModelReader
    {
        IEnumerable<Type> GetExposedModelsTypes();
    }
}
