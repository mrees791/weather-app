using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Models.OpenWeatherMap;

namespace ConsoleWeatherTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Start();
        }

        public void Start()
        {
            var oneCallRequest = new OneCallRequest();
            oneCallRequest.RequestOneCall(33.441792, -94.037689);

            Console.WriteLine("Hourly");
            for (int i = 0; i < 5; i++)
            {
                var hourly = oneCallRequest.OneCall.HourlyEntries[i];
                Console.WriteLine($"{hourly.DateTime.ToShortTimeString()} - {hourly.TemperatureCelsius}C");
            }
            Console.WriteLine("\nDaily");
            for (int i = 0; i < 7; i++)
            {
                var daily = oneCallRequest.OneCall.DailyEntries[i];
                Console.WriteLine($"{daily.DateTime.ToShortDateString()} - High: {daily.DailyTemperature.DailyHighCelsius}C Low: {daily.DailyTemperature.DailyLowCelsius}C {daily.WeatherEntries[0].Description}");
            }
        }
    }
}
