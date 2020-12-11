using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class Forecast
    {
        [JsonProperty("list")]
        public List<ForecastEntry> ForecastEntries { get; set; }
    }
}
