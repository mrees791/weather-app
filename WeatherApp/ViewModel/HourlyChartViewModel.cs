using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.FileFormats;
using WeatherLibrary.Models;
using WeatherLibrary.Models.OpenWeatherMap;

namespace WeatherApp.ViewModel
{
    public class HourlyChartViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private TemperatureFormat temperatureFormat;

        private string title;

        public HourlyChartViewModel(TemperatureFormat temperatureFormat)
        {
            this.temperatureFormat = temperatureFormat;
            SeriesCollection = new SeriesCollection();
        }

        private void CreateLabels(List<HourlyEntry> hourlyEntries, int periodAmount)
        {
            Labels = new string[periodAmount];

            for (int i = 0; i < periodAmount; i++)
            {
                Labels[i] = hourlyEntries[i].DateTime.ToShortTimeString();
            }
        }

        private ChartValues<double> CreateTemperatureValues(List<HourlyEntry> hourlyEntries, int entryAmount)
        {
            var values = new ChartValues<double>();

            for (int i = 0; i < entryAmount; i++)
            {
                var hourly = hourlyEntries[i];
                Temperature temperature = new Temperature(hourly.TemperatureCelsius, TemperatureFormat.Celsius);

                switch (temperatureFormat)
                {
                    case TemperatureFormat.Celsius:
                        values.Add(temperature.Celsius);
                        break;
                    case TemperatureFormat.Fahrenheit:
                        values.Add(temperature.Fahrenheit);
                        break;
                }
            }

            return values;
        }

        public void UpdateHourlyChart(List<HourlyEntry> hourlyEntries, int entryAmount)
        {
            SeriesCollection.Clear();
            CreateLabels(hourlyEntries, entryAmount);

            YFormatter = value => String.Format("{0:0.00}º", value);

            Title = "Temperature";

            SeriesCollection.Add(new LineSeries
            {
                Title = this.Title,
                Values = CreateTemperatureValues(hourlyEntries, entryAmount),
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 15,
                LineSmoothness = 0.1
            });
        }

        public TemperatureFormat TemperatureFormat { get => temperatureFormat; }
        public string Title { get => title; set { title = value; RaisePropertyChanged(); } }
    }
}
