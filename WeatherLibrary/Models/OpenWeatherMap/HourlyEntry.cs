using Newtonsoft.Json;
using System;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class HourlyEntry
    {
        public DateTime DateTime { get; set; }

        [JsonProperty("temp")]
        public double TemperatureCelsius { get; set; }
    }
}
