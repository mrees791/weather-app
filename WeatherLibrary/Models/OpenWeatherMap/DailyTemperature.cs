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
        public double DailyLowCelsius { get; set; }

        [JsonProperty("max")]
        public double DailyHighCelsius { get; set; }
    }
}
