using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Models.OpenWeatherMap;

namespace WeatherLibrary.Controllers.OpenWeatherMap
{
    public class OneCallController
    {
        public void RequestOneCall(double latitude, double longitude, OneCallRequest request)
        {
            request.IsValidRequest = false;

            using (var webClient = new System.Net.WebClient())
            {
                string appId = WeatherLibrary.Utility.OpenWeatherMap.ApiInfo.AppId;

                webClient.Headers.Add("user-agent", "Requesting one-call object.");
                string oneCallRequestUrl = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&units=metric&appid={2}", latitude, longitude, appId);

                string jsonOneCall = webClient.DownloadString(oneCallRequestUrl);
                request.OneCall = JsonConvert.DeserializeObject<OneCall>(jsonOneCall);

                if (request.OneCall != null)
                {
                    request.IsValidRequest = true;
                }
            }
        }
    }
}
