using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Models.OpenWeatherMap;
using WeatherLibrary.Utility;

namespace WeatherLibrary.Controllers.OpenWeatherMap
{
    public class ForecastController
    {
        public void RequestForecast(double latitude, double longitude, ForecastRequest request)
        {
            request.IsValidRequest = false;

            using (var webClient = new System.Net.WebClient())
            {
                string appId = WeatherLibrary.Utility.OpenWeatherMap.ApiInfo.AppId;

                webClient.Headers.Add("user-agent", "Requesting forecast object.");
                string forecastRequestUrl = string.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid={2}", latitude, longitude, appId);

                string jsonForecast = webClient.DownloadString(forecastRequestUrl);
                request.Forecast = JsonConvert.DeserializeObject<Forecast>(jsonForecast);

                if (request.Forecast != null)
                {
                    request.IsValidRequest = true;

                    // Create daily weather entries
                    /*var forecastEntries = request.Forecast.ForecastEntries;

                    var dayGroupedEntries = new Dictionary<DateTime, List<ForecastEntry>>();

                    foreach (var entry in forecastEntries)
                    {
                        DateTime dateTime = DateTime.Parse(entry.DateTime);

                        var dateOnly = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);

                        if (!dayGroupedEntries.ContainsKey(dateOnly))
                        {
                            dayGroupedEntries.Add(dateOnly, new List<ForecastEntry>());
                        }
                        dayGroupedEntries[dateOnly].Add(entry);
                    }

                    foreach (var group in dayGroupedEntries)
                    {
                        var dayForecastEntries = group.Value;

                        var dateTime = dayForecastEntries[0].DateTime;
                        double minCelsius = dayForecastEntries.Min(e => e.Main.TemperatureCelsiusMin);
                        double maxCelsius = dayForecastEntries.Max(e => e.Main.TemperatureCelsiusMax);
                        double minFahrenheit = TemperatureUtility.ConvertCelsiusToFahrenheit(minCelsius);
                        double maxFahrenheit = TemperatureUtility.ConvertCelsiusToFahrenheit(maxCelsius);

                        var dailyEntry = new DailyEntry(dateTime, minCelsius, maxCelsius, minFahrenheit, maxFahrenheit, dayForecastEntries[0].Weather[0]);

                        request.DailyEntries.Add(dailyEntry);
                    }*/
                }
            }
        }
    }
}
