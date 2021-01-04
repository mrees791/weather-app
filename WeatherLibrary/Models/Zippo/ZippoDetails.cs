using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherLibrary.Models.Zippo
{
    public class ZippoDetails
    {
        [JsonProperty("post code")]
        public string ZipCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("places")]
        public List<Place> Places { get; set; }

        public Place GetZipPlace()
        {
            return Places[0];
        }
    }
}
