using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.UI;
using WeatherLibrary.Models;

namespace WeatherApp.ViewModel
{
    public class DayWeatherViewModel : ViewModelBase
    {
        private string date;
        private string temperatureCelsiusMin;
        private string temperatureCelsiusMax;
        private string temperatureFahrenheitMin;
        private string temperatureFahrenheitMax;
        private string description;
        private WeatherType weatherType;
        private WeatherIcon weatherIcon;
        private string iconUrl;

        public DayWeatherViewModel()
        {
            WeatherType = WeatherType.Unknown;
        }

        public string Date { get => date; set { date = value; RaisePropertyChanged(); } }
        public string TemperatureCelsiusMin { get => temperatureCelsiusMin; set { temperatureCelsiusMin = value; RaisePropertyChanged(); } }
        public string TemperatureCelsiusMax { get => temperatureCelsiusMax; set { temperatureCelsiusMax = value; RaisePropertyChanged(); } }
        public string TemperatureFahrenheitMin { get => temperatureFahrenheitMin; set { temperatureFahrenheitMin = value; RaisePropertyChanged(); } }
        public string TemperatureFahrenheitMax { get => temperatureFahrenheitMax; set { temperatureFahrenheitMax = value; RaisePropertyChanged(); } }
        public string Description { get => description; set { description = value; RaisePropertyChanged(); } }

        public string IconUrl { get => iconUrl; private set { iconUrl = value; RaisePropertyChanged(); } }

        public WeatherType WeatherType { get => weatherType; set { weatherType = value; weatherIcon = new WeatherIcon(WeatherType); IconUrl = weatherIcon.IconUrl; } }
    }
}
