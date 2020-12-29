using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class HourlyEntry
    {
        public DateTime DateTime { get; set; }

        [JsonProperty("temp")]
        public double TemperatureCelsius { get; set; }
    }
}
