using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Models.Zippo;

namespace WeatherLibrary.Controllers.Zippo
{
    public class ZippoController
    {
        public void RequestZipCodeDetails(string zipCode, ZippoRequest request)
        {
            request.IsValidRequest = false;
            request.ErrorMessage = string.Empty;
            string zipApiUrl = string.Format("http://api.zippopotam.us/us/{0}", zipCode);

            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    string jsonZip = webClient.DownloadString(zipApiUrl);
                    request.Details = JsonConvert.DeserializeObject<ZippoDetails>(jsonZip);
                    request.IsValidRequest = true;
                }
                catch (Exception ex)
                {
                    request.ErrorMessage = ex.Message;
                }
            }
        }
    }
}
