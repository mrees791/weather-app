using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.Zippo
{
    public class ZippoRequest
    {
        private ZippoDetails details;

        public ZippoRequest()
        {

        }

        public void RequestZipCodeDetails(string zipCode)
        {
            string zipApiUrl = $"http://api.zippopotam.us/us/{zipCode}";

            using (var webClient = new System.Net.WebClient())
            {
                string jsonZip = webClient.DownloadString(zipApiUrl);
                details = JsonConvert.DeserializeObject<ZippoDetails>(jsonZip);
            }
        }

        public ZippoDetails Details { get => details; }
    }
}
