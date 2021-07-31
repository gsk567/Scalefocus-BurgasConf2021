using BurgasConf.Generator.Exceptions;
using BurgasConf.Generator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BurgasConf.Generator.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/dev/generation/")]
    public class GenerationController : ControllerBase
    {
        private readonly IGenerationProvider generationProvider;

        public GenerationController(IHostEnvironment hostEnvironment, IGenerationProvider generationProvider)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                throw new DevelopmentOnlyException("Runtime generation is available only in development environment.");
            }

            this.generationProvider = generationProvider;
        }

        [HttpGet]
        [Route("run")]
        public IActionResult Generate()
        {
            var result = this.generationProvider.Generate();
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}