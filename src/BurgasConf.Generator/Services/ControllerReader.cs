using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using BurgasConf.Generator.Extensions;
using BurgasConf.Generator.Models;
using BurgasConf.Generator.Options;
using Definux.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BurgasConf.Generator.Services
{
    public class ControllerReader : IControllerReader
    {
        private readonly GeneratorOptions generatorOptions;

        public ControllerReader(IOptions<GeneratorOptions> generatorOptionsAccessor)
        {
            this.generatorOptions = generatorOptionsAccessor.Value;
        }

        public IEnumerable<ControllerAction> GetExposedActions()
        {
            var result = new List<ControllerAction>();

            try
            {
                if (this.generatorOptions.SourceAssemblies == null || this.generatorOptions.SourceAssemblies.Count <= 0)
                {
                    return result;
                }

                var controllerTypes = new List<Type>();
                foreach (var assembly in this.generatorOptions.SourceAssemblies)
                {
                    controllerTypes.AddRange(assembly
                        .GetTypes()
                        .Where(x => x.BaseType != null &&
                                    x.BaseType == typeof(ControllerBase) &&
                                    x.IsSourceForGeneration()
                        )
                    );
                }

                if (controllerTypes.Any())
                {
                    foreach (var controllerType in controllerTypes)
                    {
                        var actions = controllerType.GetMethods().Where(x => x.IsSourceForGeneration());
                        foreach (var action in actions)
                        {
                            result.Add(new ControllerAction
                            {
                                ActionName = action.Name,
                                ControllerName = controllerType.Name,
                                ControllerRoute = controllerType.GetAttribute<RouteAttribute>()?.Template,
                                ActionRoute = action.GetAttribute<RouteAttribute>()?.Template,
                                ResponseType = action.ReturnType,
                                Method = GetMethodForAction(action),
                                Authorized = action.HasAttribute<AuthorizeAttribute>() ||
                                             (controllerType.HasAttribute<AuthorizeAttribute>() && action.HasAttribute<AllowAnonymousAttribute>()),
                                Parameters = action.GetParameters().ToDictionary(k => k.Name, v => v.ParameterType)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        private HttpMethod GetMethodForAction(MethodInfo methodInfo)
        {
            HttpMethod method = HttpMethod.Get;

            if (methodInfo.HasAttribute<HttpPostAttribute>())
            {
                method = HttpMethod.Post;
            }
            else if (methodInfo.HasAttribute<HttpPutAttribute>())
            {
                method = HttpMethod.Put;
            }
            else if (methodInfo.HasAttribute<HttpDeleteAttribute>())
            {
                method = HttpMethod.Delete;
            }

            return method;
        }
    }
}