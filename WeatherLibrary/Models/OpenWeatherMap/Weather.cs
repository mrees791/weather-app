using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class Weather
    {
        [JsonProperty("main")]
        public string Description { get; set; }
        [JsonProperty("description")]
        public string DetailDescription { get; set; }
        [JsonProperty("icon")]
        public string IconId { get; set; }

        public string GetIconUrl()
        {
            return $"http://openweathermap.org/img/wn/{IconId}@4x.png";
        }
    }
}
