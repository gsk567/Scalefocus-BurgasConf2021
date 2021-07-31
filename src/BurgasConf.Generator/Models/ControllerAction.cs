using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace BurgasConf.Generator.Models
{
    public class ControllerAction
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public HttpMethod Method { get; set; }

        public string Route =>
            ActionRoute.StartsWith("/", StringComparison.OrdinalIgnoreCase)
                ? ActionRoute
                : $"{ControllerRoute}{ActionRoute}";

        public string ActionRoute { get; set; }

        public string ControllerRoute { get; set; }

        [JsonIgnore]
        public Type ResponseType { get; set; }

        public bool Authorized { get; set; }

        [JsonIgnore]
        public Dictionary<string, Type> Parameters { get; set; }
    }
}