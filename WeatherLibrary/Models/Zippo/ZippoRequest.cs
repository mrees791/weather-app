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
        private bool isValidRequest;
        private string errorMessage;
        private ZippoDetails details;

        public ZippoRequest()
        {

        }

        public void RequestZipCodeDetails(string zipCode)
        {
            string zipApiUrl = $"http://api.zippopotam.us/us/{zipCode}";
            isValidRequest = false;
            errorMessage = string.Empty;

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    string jsonZip = webClient.DownloadString(zipApiUrl);
                    details = JsonConvert.DeserializeObject<ZippoDetails>(jsonZip);
                    isValidRequest = true;
                }
            }
            catch (System.Net.WebException ex)
            {
                errorMessage = ex.Message;
            }
        }

        public ZippoDetails Details { get => details; }
        public bool IsValidRequest { get => isValidRequest; }
        public string ErrorMessage { get => errorMessage; }
    }
}
