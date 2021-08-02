using System;
using BurgasConf.Generator.Attributes;
using BurgasConf.Generator.Models;

namespace BurgasConf.Api
{
    [GeneratorSource]
    public class WeatherForecast : IModel
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public string Note { get; set; } = "BurgasConf2021";
    }
}