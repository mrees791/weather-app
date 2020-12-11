using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Models;
using WeatherLibrary.Models.WeatherGov;

namespace WeatherLibrary.Controllers.WeatherGov
{
    public class HourlyController
    {
        /// <summary>
        /// Requests an hourly object through the weather.gov API by using the latitude and longitude coordinates from zip code details.
        /// </summary>
        /// <param name="zipCodeDetails"></param>
        /// <param name="request"></param>
        public void RequestHourly(double latitude, double longitude, HourlyRequest request)
        {
            Points points = null;
            request.IsValidRequest = false;
            request.ErrorMessage = string.Empty;

            using (var webClient = new System.Net.WebClient())
            {

                webClient.Headers.Add("user-agent", "Requesting points properties object.");
                string pointsRequestUrl = string.Format("https://api.weather.gov/points/{0},{1}", latitude, longitude);

                try
                {
                    string jsonPoints = webClient.DownloadString(pointsRequestUrl);
                    points = JsonConvert.DeserializeObject<Points>(jsonPoints);
                }
                catch (WebException nex)
                {
                    request.ErrorMessage = nex.Message;
                }
                catch (Exception ex)
                {
                    request.ErrorMessage = ex.Message;
                }
            }
            if (points != null)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Headers.Add("user-agent", "Requesting hourly object.");

                    string jsonHourly = webClient.DownloadString(points.Properties.ForecastHourly);
                    request.Hourly = JsonConvert.DeserializeObject<Hourly>(jsonHourly);



                    // Create highs/lows from entries
                    //var details = request.Hourly.Properties.Periods;

                    request.IsValidRequest = true;
                }
            }
        }
    }
}
