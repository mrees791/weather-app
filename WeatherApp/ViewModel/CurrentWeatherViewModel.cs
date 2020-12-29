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
    public class CurrentWeatherViewModel : ViewModelBase
    {
        private string lastUpdateTime;
        private string city;
        private string state;
        private string temperatureFahrenheit;
        private string temperatureCelsius;
        private string shortForecast;

        public void UpdateCurrentWeather(OneCallRequest oneCallRequest, ZippoRequest zipCodeRequest)
        {
            LastUpdateTime = string.Format("{0:hh:mm:ss tt}", oneCallRequest.RequestDateTime);
            var currentWeather = oneCallRequest.OneCall.CurrentWeather;
            Temperature temperature = new Temperature(currentWeather.TemperatureCelsius, TemperatureFormat.Celsius);
            Place place = zipCodeRequest.Details.Places[0];
            City = place.CityName;
            State = place.State;
            TemperatureFahrenheit = string.Format("{0:0.##}º F", temperature.Fahrenheit);
            TemperatureCelsius = string.Format("{0:0.##}º C", temperature.Celsius);
            ShortForecast = currentWeather.WeatherEntries[0].Description;
        }

        public string LastUpdateTime { get => lastUpdateTime; set { lastUpdateTime = value; RaisePropertyChanged(); } }

        public string City { get => city; set { city = value; RaisePropertyChanged(); } }
        public string State { get => state; set { state = value; RaisePropertyChanged(); } }
        public string TemperatureFahrenheit { get => temperatureFahrenheit; set { temperatureFahrenheit = value; RaisePropertyChanged(); } }
        public string ShortForecast { get => shortForecast; set { shortForecast = value; RaisePropertyChanged(); } }

        public string TemperatureCelsius { get => temperatureCelsius; set { temperatureCelsius = value; RaisePropertyChanged(); } }
    }
}
