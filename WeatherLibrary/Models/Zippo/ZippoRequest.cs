using Newtonsoft.Json;

namespace WeatherLibrary.Models.Zippo
{
    /// <summary>
    /// The class used to request zip code data from the Zippopatum.US Zip API.
    /// Stores the ZippoDetails data received from the API and if the request was valid.
    /// </summary>
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
