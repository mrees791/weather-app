using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    /// <summary>
    /// The root object that is requested from the OpenWeatherMap API.
    /// </summary>
    public class OneCall
    {
        [JsonProperty("hourly")]
        public List<HourlyEntry> HourlyEntries { get; set; }

        [JsonProperty("daily")]
        public List<DailyEntry> DailyEntries { get; set; }

        [JsonProperty("current")]
        public CurrentWeather CurrentWeather { get; set; }
    }
}
