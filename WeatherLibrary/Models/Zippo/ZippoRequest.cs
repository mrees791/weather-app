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
            IsValidRequest = false;
            ErrorMessage = string.Empty;
            string zipApiUrl = string.Format("http://api.zippopotam.us/us/{0}", zipCode);

            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    string jsonZip = webClient.DownloadString(zipApiUrl);
                    Details = JsonConvert.DeserializeObject<ZippoDetails>(jsonZip);
                    IsValidRequest = true;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
            }
        }

        public bool IsValidRequest { get => isValidRequest; set => isValidRequest = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public ZippoDetails Details { get => details; set => details = value; }
    }
}
