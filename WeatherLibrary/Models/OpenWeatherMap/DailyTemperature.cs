using Newtonsoft.Json;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class DailyTemperature
    {
        [JsonProperty("min")]
        public double DailyLowCelsius { get; set; }

        [JsonProperty("max")]
        public double DailyHighCelsius { get; set; }
    }
}
