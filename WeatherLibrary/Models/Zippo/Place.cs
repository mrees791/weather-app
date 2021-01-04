using Newtonsoft.Json;

namespace WeatherLibrary.Models.Zippo
{
    public class Place
    {
        [JsonProperty("place name")]
        public string CityName { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }
    }
}
