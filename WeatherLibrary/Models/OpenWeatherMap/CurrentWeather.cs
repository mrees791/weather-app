using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class CurrentWeather
    {
        public DateTime DateTime { get; set; }

        [JsonProperty("temp")]
        public double TemperatureCelsius { get; set; }

        [JsonProperty("weather")]
        public List<Weather> WeatherEntries { get; set; }
    }
}
