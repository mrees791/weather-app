using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Models;
using WeatherLibrary.Models.OpenWeatherMap;
using WeatherLibrary.Models.Zippo;

namespace WeatherApp.ViewModel
{
    public class FavoritesBoxViewModel : ViewModelBase
    {
        private ZippoRequest zipRequest;
        private OneCallRequest oneCallRequest;
        private Place place;

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

        public FavoritesBoxViewModel(string zipCode)
        {
            this.zipCode = zipCode;
            oneCallRequest = new OneCallRequest();
            zipRequest = new ZippoRequest();

            UpdateZip();
        }

        private void UpdateZip()
        {
            zipRequest.RequestZipCodeDetails(zipCode);

            place = zipRequest.Details.Places[0];
            city = place.CityName;
            state = place.State;
        }

        public void UpdateCurrentWeather()
        {
            if (zipRequest.Details == null)
            {
                UpdateZip();
            }

            oneCallRequest.RequestOneCall(place.Latitude, place.Longitude);
            var currentWeather = oneCallRequest.OneCall.CurrentWeather;
            var daily = oneCallRequest.OneCall.DailyEntries[0];

            Temperature temperature = new Temperature(currentWeather.TemperatureCelsius, TemperatureFormat.Celsius);
            Temperature dailyHigh = new Temperature(daily.DailyTemperature.DailyHighCelsius, TemperatureFormat.Celsius);
            Temperature dailyLow = new Temperature(daily.DailyTemperature.DailyLowCelsius, TemperatureFormat.Celsius);

            CurrentWeatherFahrenheit = string.Format("{0:0.##}", temperature.Fahrenheit);
            CurrentWeatherCelsius = string.Format("{0:0.##}", temperature.Celsius);

            double highCelsius = dailyHigh.Celsius;
            double lowCelsius = dailyLow.Celsius;
            double highFahrenheit = dailyHigh.Fahrenheit;
            double lowFahrenheit = dailyLow.Fahrenheit;

            DailyHighCelsius = string.Format("{0:0.##}", highCelsius);
            DailyLowCelsius = string.Format("{0:0.##}", lowCelsius);
            DailyHighFahrenheit = string.Format("{0:0.##}", highFahrenheit);
            DailyLowFahrenheit = string.Format("{0:0.##}", lowFahrenheit);

            ShortForecast = currentWeather.WeatherEntries[0].Description;
        }


        public string ZipCode { get => zipCode; set { zipCode = value; RaisePropertyChanged(); } }
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
