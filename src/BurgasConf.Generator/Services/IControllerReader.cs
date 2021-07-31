using System.Collections.Generic;
using BurgasConf.Generator.Models;

namespace BurgasConf.Generator.Services
{
    public interface IControllerReader
    {
        IEnumerable<ControllerAction> GetExposedActions();
    }
}