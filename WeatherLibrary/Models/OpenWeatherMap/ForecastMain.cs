using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class ForecastMain
    {
        [JsonProperty("temp_min")]
        public double TemperatureCelsiusMin { get; set; }
        [JsonProperty("temp_max")]
        public double TemperatureCelsiusMax { get; set; }
    }
}
