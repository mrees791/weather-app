using Newtonsoft.Json;
using System;
using System.Configuration;
using WeatherLibrary.Models.OpenWeatherMap.Exceptions;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    /// <summary>
    /// The class used to send a OneCall request to the OpenWeatherMap Weather API.
    /// Also stores the datetime of the request and if the request was valid.
    /// </summary>
    public class OneCallRequest
    {
        private DateTime requestDateTime;
        private OneCall oneCall;
        private bool isValidRequest;

        public OneCallRequest()
        {

        }


        public void RequestOneCall(double latitude, double longitude)
        {
            isValidRequest = false;
            oneCall = null;

            using (var webClient = new System.Net.WebClient())
            {
                string apiKey = ConfigurationManager.AppSettings["openWeatherMapApiKey"];

                if (apiKey == null)
                {
                    throw new NullApiKeyException("OpenWeatherMap API key was not found. Make sure App.config has the correct appSettings file.");
                }

                webClient.Headers.Add("user-agent", "Requesting one-call object.");
                string oneCallRequestUrl = $"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=minutely,alerts&units=metric&appid={apiKey}";
                string jsonOneCall = webClient.DownloadString(oneCallRequestUrl);
                oneCall = JsonConvert.DeserializeObject<OneCall>(jsonOneCall);

                var now = DateTime.Now;
                requestDateTime = now;
                oneCall.CurrentWeather.DateTime = now;
                DateTime firstHourTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
                DateTime firstDayDate = new DateTime(now.Year, now.Month, now.Day);

                // Set date time in hourly.
                for (int iHour = 0; iHour < oneCall.HourlyEntries.Count; iHour++)
                {
                    var hour = oneCall.HourlyEntries[iHour];
                    hour.DateTime = firstHourTime.AddHours(iHour);
                }

                // Set date time in daily.
                for (int iDay = 0; iDay < oneCall.DailyEntries.Count; iDay++)
                {
                    var daily = oneCall.DailyEntries[iDay];
                    daily.DateTime = firstDayDate.AddDays(iDay);
                }
                isValidRequest = true;
            }
        }

        public OneCall OneCall { get => oneCall; }
        public DateTime RequestDateTime { get => requestDateTime; }
        public bool IsValidRequest { get => isValidRequest; }
    }
}
