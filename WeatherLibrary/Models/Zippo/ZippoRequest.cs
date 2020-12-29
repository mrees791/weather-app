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
        private bool isValidRequest;

        public ZippoRequest()
        {

        }

        public void RequestZipCodeDetails(string zipCode)
        {
            isValidRequest = false;
            details = null;
            string zipApiUrl = $"http://api.zippopotam.us/us/{zipCode}";

            using (var webClient = new System.Net.WebClient())
            {
                string jsonZip = webClient.DownloadString(zipApiUrl);
                details = JsonConvert.DeserializeObject<ZippoDetails>(jsonZip);
                isValidRequest = true;
            }
        }

        public ZippoDetails Details { get => details; }
        public bool IsValidRequest { get => isValidRequest; }
    }
}
