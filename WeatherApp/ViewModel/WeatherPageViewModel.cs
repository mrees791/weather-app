﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using WeatherLibrary.Models;
using WeatherLibrary.Models.OpenWeatherMap;
using WeatherLibrary.Models.OpenWeatherMap.Exceptions;
using WeatherLibrary.Models.Zippo;

namespace WeatherApp.ViewModel
{
    public class WeatherPageViewModel : ViewModelBase
    {
        private string currentZip;
        public readonly int NumberOfDailyEntries = 5;
        public readonly int HourlyEntryAmount = 7;
        private bool hasError;
        private string errorMessage;
        private HourlyChartViewModel hourlyChartCelsiusVm;
        private HourlyChartViewModel hourlyChartFahrenheitVm;
        private CurrentWeatherViewModel currentWeatherVm;
        private ObservableCollection<DayWeatherViewModel> dayWeatherViewModels;

        private ZippoRequest zipRequest;
        private OneCallRequest oneCallRequest;

        public WeatherPageViewModel()
        {
            zipRequest = new ZippoRequest();
            oneCallRequest = new OneCallRequest();

            hourlyChartCelsiusVm = new HourlyChartViewModel(TemperatureFormat.Celsius);
            hourlyChartFahrenheitVm = new HourlyChartViewModel(TemperatureFormat.Fahrenheit);
            currentWeatherVm = new CurrentWeatherViewModel();
            dayWeatherViewModels = new ObservableCollection<DayWeatherViewModel>();

            for (int i = 0; i < NumberOfDailyEntries; i++)
            {
                var dayWeatherVm = new DayWeatherViewModel();
                dayWeatherViewModels.Add(dayWeatherVm);
            }

            HasError = true;
            ErrorMessage = "No zip code requested.";

            if (IsInDesignMode)
            {
                HasError = false;
                ErrorMessage = "Weather page error message.";
            }
        }

        public void RefreshWeatherData()
        {
            UpdateWeatherData(currentZip);
        }

        public void UpdateWeatherData(string zipInput)
        {
            HasError = false;
            ErrorMessage = string.Empty;
            try
            {
                zipRequest.RequestZipCodeDetails(zipInput);
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = "Error retrieving zip-code data.\nMake sure you entered a valid U.S. zip-code.";
            }

            if (zipRequest.IsValidRequest)
            {
                Place place = zipRequest.Details.GetZipPlace();

                try
                {
                    oneCallRequest.RequestOneCall(place.Latitude, place.Longitude);
                }
                catch (NullApiKeyException nullKeyEx)
                {
                    HasError = true;
                    ErrorMessage = nullKeyEx.Message;
                }
                catch (Exception ex)
                {
                    HasError = true;
                    ErrorMessage = "Error retrieving weather data.";
                }

                if (oneCallRequest.IsValidRequest)
                {
                    List<HourlyEntry> hourlyEntries = oneCallRequest.OneCall.HourlyEntries;

                    hourlyChartFahrenheitVm.UpdateHourlyChart(hourlyEntries, HourlyEntryAmount);
                    hourlyChartCelsiusVm.UpdateHourlyChart(hourlyEntries, HourlyEntryAmount);

                    UpdateDailyForecast();
                    currentWeatherVm.UpdateCurrentWeather(oneCallRequest, zipRequest);

                    currentZip = zipInput;
                }
            }
        }


        private void UpdateDailyForecast()
        {
            List<DailyEntry> dailies = oneCallRequest.OneCall.DailyEntries;

            for (int i = 0; i < dayWeatherViewModels.Count; i++)
            {
                DailyEntry daily = dailies[i];
                Weather weather = daily.WeatherEntries[0];

                Temperature temperatureHigh = new Temperature(daily.DailyTemperature.DailyHighCelsius, TemperatureFormat.Celsius);
                Temperature temperatureLow = new Temperature(daily.DailyTemperature.DailyLowCelsius, TemperatureFormat.Celsius);

                DayWeatherViewModel vm = dayWeatherViewModels[i];
                vm.Date = FormatDateString(daily.DateTime);
                vm.Description = GetFinalDescription(weather.Description);
                vm.TemperatureCelsiusLow = string.Format("{0:0.00}", temperatureLow.Celsius);
                vm.TemperatureCelsiusHigh = string.Format("{0:0.00}", temperatureHigh.Celsius);
                vm.TemperatureFahrenheitLow = string.Format("{0:0.00}", temperatureLow.Fahrenheit);
                vm.TemperatureFahrenheitHigh = string.Format("{0:0.00}", temperatureHigh.Fahrenheit);
                vm.IconUrl = weather.GetIconUrl();
            }
        }

        /// <summary>
        /// Changes the view model description. Example: Changes "Clear" to "Sunny"
        /// </summary>
        /// <param name="originalDescription"></param>
        /// <returns></returns>
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

        private string FormatDateString(DateTime date)
        {
            return string.Format("{0} {1}", CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(date.Month), date.Day);
        }

        public ObservableCollection<DayWeatherViewModel> DayWeatherViewModels { get => dayWeatherViewModels; }
        public CurrentWeatherViewModel CurrentWeatherVm { get => currentWeatherVm; }
        public HourlyChartViewModel HourlyChartCelsiusVm { get => hourlyChartCelsiusVm; }
        public HourlyChartViewModel HourlyChartFahrenheitVm { get => hourlyChartFahrenheitVm; }
        public bool HasError { get => hasError; set { hasError = value; RaisePropertyChanged(); } }
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; RaisePropertyChanged(); } }
        public string CurrentZip { get => currentZip; private set => currentZip = value; }
    }
}
