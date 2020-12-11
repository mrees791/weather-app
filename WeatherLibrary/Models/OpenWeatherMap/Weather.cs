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

        public WeatherType GetWeatherType()
        {
            switch (Description)
            {
                case "Clear":
                    return WeatherType.Clear;
                case "Rain":
                    return WeatherType.Rain;
                case "Snow":
                    return WeatherType.Snow;
                case "Clouds":
                    return WeatherType.Cloudy;
                default:
                    return WeatherType.Unknown;
            }
        }
    }
}
