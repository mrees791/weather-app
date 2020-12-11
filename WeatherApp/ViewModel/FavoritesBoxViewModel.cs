using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Controllers.OpenWeatherMap;
using WeatherLibrary.Controllers.WeatherGov;
using WeatherLibrary.Controllers.Zippo;
using WeatherLibrary.Models.OpenWeatherMap;
using WeatherLibrary.Models.WeatherGov;
using WeatherLibrary.Models.Zippo;
using WeatherLibrary.Utility;

namespace WeatherApp.ViewModel
{
    public class FavoritesBoxViewModel : ViewModelBase
    {
        private OneCallRequest oneCallRequest;
        private ZippoRequest zipRequest;
        private HourlyRequest hourlyRequest;

        private OneCallController oneCallController;
        private ZippoController zipController;
        private HourlyController hourlyController;

        private string zipCode;
        private string city;
        private string state;

        private string currentWeatherFahrenheit;
        private string dailyHighFahrenheit;
        private string dailyLowFahrenheit;

        private string currentWeatherCelsius;
        private string dailyHighCelsius;
        private string dailyLowCelsius;

        private string shortForecast;

        public FavoritesBoxViewModel()
        {
            oneCallRequest = new OneCallRequest();
            zipRequest = new ZippoRequest();
            hourlyRequest = new HourlyRequest();

            oneCallController = new OneCallController();
            zipController = new ZippoController();
            hourlyController = new HourlyController();
        }

        private void UpdateZipInfo()
        {
            zipController.RequestZipCodeDetails(zipCode, zipRequest);

            Place place = zipRequest.Details.Places[0];

            City = place.CityName;
            State = place.State;
        }

        public void UpdateCurrentWeather()
        {
            Place place = zipRequest.Details.Places[0];

            hourlyController.RequestHourly(place.Latitude, place.Longitude, hourlyRequest);
            oneCallController.RequestOneCall(place.Latitude, place.Longitude, oneCallRequest);

            var period = hourlyRequest.Hourly.Properties.Periods[0];

            bool periodIsFahrenheit = period.TemperatureUnit == "F";

            if (periodIsFahrenheit)
            {
                double currentFahrenheit = period.Temperature;
                double currentCelsius = TemperatureUtility.ConvertFahrenheitToCelsuis(currentFahrenheit);

                CurrentWeatherFahrenheit = string.Format("{0:0.##}", currentFahrenheit);
                CurrentWeatherCelsius = string.Format("{0:0.##}", currentCelsius);
            }
            else
            {
                double currentCelsius = period.Temperature;
                double currentFahrenheit = TemperatureUtility.ConvertCelsiusToFahrenheit(currentCelsius);

                CurrentWeatherFahrenheit = string.Format("{0:0.##}", currentFahrenheit);
                CurrentWeatherCelsius = string.Format("{0:0.##}", currentCelsius);
            }
            double highCelsius = oneCallRequest.OneCall.DailyEntries[0].DailyTemperature.DailyMaxCelsius;
            double lowCelsius = oneCallRequest.OneCall.DailyEntries[0].DailyTemperature.DailyMinCelsius;
            double highFahrenheit = TemperatureUtility.ConvertCelsiusToFahrenheit(highCelsius);
            double lowFahrenheit = TemperatureUtility.ConvertCelsiusToFahrenheit(lowCelsius);

            DailyHighCelsius = string.Format("{0:0.##}", highCelsius);
            DailyLowCelsius = string.Format("{0:0.##}", lowCelsius);
            DailyHighFahrenheit = string.Format("{0:0.##}", highFahrenheit);
            DailyLowFahrenheit = string.Format("{0:0.##}", lowFahrenheit);

            ShortForecast = period.ShortForecast;
        }


        public string ZipCode { get => zipCode; set { zipCode = value; UpdateZipInfo(); RaisePropertyChanged(); } }
        public string City { get => city; set { city = value; RaisePropertyChanged(); } }
        public string State { get => state; set { state = value; RaisePropertyChanged(); } }

        public string CurrentWeatherFahrenheit { get => currentWeatherFahrenheit; set { currentWeatherFahrenheit = value; RaisePropertyChanged(); } }
        public string DailyHighFahrenheit { get => dailyHighFahrenheit; set { dailyHighFahrenheit = value; RaisePropertyChanged(); } }
        public string DailyLowFahrenheit { get => dailyLowFahrenheit; set { dailyLowFahrenheit = value; RaisePropertyChanged(); } }

        public string ShortForecast { get => shortForecast; set { shortForecast = value; RaisePropertyChanged(); } }

        public string CurrentWeatherCelsius { get => currentWeatherCelsius; set { currentWeatherCelsius = value; RaisePropertyChanged(); } }
        public string DailyHighCelsius { get => dailyHighCelsius; set { dailyHighCelsius = value; RaisePropertyChanged(); } }
        public string DailyLowCelsius { get => dailyLowCelsius; set { dailyLowCelsius = value; RaisePropertyChanged(); } }
    }
}
