using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class OneCallRequest
    {
        private bool isValidRequest;
        private string errorMessage;
        private OneCall oneCall;

        public OneCallRequest()
        {

        }


        public void RequestOneCall(double latitude, double longitude)
        {
            isValidRequest = false;
            errorMessage = string.Empty;

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    string appId = ApiInfo.AppId;

                    webClient.Headers.Add("user-agent", "Requesting one-call object.");
                    string oneCallRequestUrl = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude=minutely,alerts&units=metric&appid={2}", latitude, longitude, appId);

                    string jsonOneCall = webClient.DownloadString(oneCallRequestUrl);
                    oneCall = JsonConvert.DeserializeObject<OneCall>(jsonOneCall);

                    var now = DateTime.Now;

                    oneCall.RequestDateTime = now;
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
            catch (System.Net.WebException ex)
            {
                errorMessage = ex.Message;
            }
        }

        public bool IsValidRequest { get => isValidRequest; }
        public string ErrorMessage { get => errorMessage; }
        public OneCall OneCall { get => oneCall; }
    }
}
