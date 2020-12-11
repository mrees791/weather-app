using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class DailyTemperature
    {
        [JsonProperty("min")]
        public double DailyMinCelsius { get; set; }

        [JsonProperty("max")]
        public double DailyMaxCelsius { get; set; }
    }
}
