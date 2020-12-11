using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class DailyEntry
    {
        [JsonProperty("temp")]
        public DailyTemperature DailyTemperature { get; set; }

        [JsonProperty("weather")]
        public List<Weather> WeatherEntries { get; set; }
    }
}
