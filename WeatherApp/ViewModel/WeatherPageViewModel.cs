using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherApp.Utility;
using WeatherLibrary.Models;
using WeatherLibrary.Models.OpenWeatherMap;

namespace WeatherApp.ViewModel
{
    public class WeatherPageViewModel : ViewModelBase
    {
        private ObservableCollection<DayWeatherViewModel> dayWeatherViewModels;
        public readonly int NumberOfDailyEntries = 5;

        public WeatherPageViewModel()
        {
            string iconDir = AppDirectories.IconDirectory;
            dayWeatherViewModels = new ObservableCollection<DayWeatherViewModel>();

            for (int i = 0; i < NumberOfDailyEntries; i++)
            {
                var dayWeatherVm = new DayWeatherViewModel();
                dayWeatherVm.IconUrl = GetIconUrl(dayWeatherVm);
                dayWeatherViewModels.Add(dayWeatherVm);
            }
        }

        public ObservableCollection<DayWeatherViewModel> DayWeatherViewModels { get => dayWeatherViewModels; }

        public void UpdateDailyForecast(OneCallRequest request)
        {
            var dailies = request.OneCall.DailyEntries;

            for (int i = 0; i < dayWeatherViewModels.Count; i++)
            {
                var daily = dailies[i];
                var vm = dayWeatherViewModels[i];

                var weather = daily.WeatherEntries[0];

                vm.Date = FormatDateString(daily.DateTime);

                var temperatureHigh = new Temperature(daily.DailyTemperature.DailyHighCelsius, TemperatureFormat.Celsius);
                var temperatureLow = new Temperature(daily.DailyTemperature.DailyLowCelsius, TemperatureFormat.Celsius);

                vm.Description = GetFinalDescription(weather.Description);
                vm.WeatherType = weather.GetWeatherType();
                vm.TemperatureCelsiusMin = string.Format("{0:0.00}", temperatureLow.Celsius);
                vm.TemperatureCelsiusMax = string.Format("{0:0.00}", temperatureHigh.Celsius);
                vm.TemperatureFahrenheitMin = string.Format("{0:0.00}", temperatureLow.Fahrenheit);
                vm.TemperatureFahrenheitMax = string.Format("{0:0.00}", temperatureHigh.Fahrenheit);
                vm.IconUrl = GetIconUrl(vm);
            }
        }

        // Changes the view model description. Example: Changes "Clear" to "Sunny"
        private string GetFinalDescription(string originalDescription)
        {
            switch (originalDescription)
            {
                case "Clear":
                    return "Sunny";
                case "Clouds":
                    return "Cloudy";
            }

            return originalDescription;
        }

        private string GetIconUrl(DayWeatherViewModel dayWeatherVm)
        {
            string iconDirectory = AppDirectories.IconDirectory;

            switch (dayWeatherVm.WeatherType)
            {
                case WeatherLibrary.Models.WeatherType.Clear:
                    return iconDirectory + "sun1.xaml";
                case WeatherLibrary.Models.WeatherType.Cloudy:
                    return iconDirectory + "cloud1.xaml";
                case WeatherLibrary.Models.WeatherType.Rain:
                    return iconDirectory + "rain1.xaml";
                case WeatherLibrary.Models.WeatherType.Snow:
                    return iconDirectory + "snow1.xaml";
            }

            return iconDirectory + "unknown.xaml";
        }

        private string FormatDateString(DateTime date)
        {
            return string.Format("{0} {1}", CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(date.Month), date.Day);
        }
    }
}
