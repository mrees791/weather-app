using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class OneCall
    {
        [JsonProperty("hourly")]
        public List<HourlyEntry> HourlyEntries { get; set; }

        [JsonProperty("daily")]
        public List<DailyEntry> DailyEntries { get; set; }

        [JsonProperty("current")]
        public CurrentWeather CurrentWeather { get; set; }

        public DateTime RequestDateTime { get; set; }
    }
}
