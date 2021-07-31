using System.Threading.Tasks;
using BurgasConf.Generator.Models;

namespace BurgasConf.Generator.Services
{
    public interface IGenerationProvider
    {
        GenerationResult Generate();
    }
}