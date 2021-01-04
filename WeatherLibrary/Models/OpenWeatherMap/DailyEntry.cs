using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class DailyEntry
    {
        public DateTime DateTime { get; set; }

        [JsonProperty("temp")]
        public DailyTemperature DailyTemperature { get; set; }

        [JsonProperty("weather")]
        public List<Weather> WeatherEntries { get; set; }
    }
}
