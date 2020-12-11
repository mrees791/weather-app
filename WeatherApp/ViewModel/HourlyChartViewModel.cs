using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.FileFormats;
using WeatherLibrary.Controllers;
using WeatherLibrary.Controllers.WeatherGov;
using WeatherLibrary.Models;
using WeatherLibrary.Models.WeatherGov;
using WeatherLibrary.Utility;

namespace WeatherApp.ViewModel
{
    public class HourlyChartViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private PeriodController periodController;
        private TemperatureFormat temperatureFormat;

        private string title;

        public HourlyChartViewModel(TemperatureFormat temperatureFormat)
        {
            this.temperatureFormat = temperatureFormat;
            SeriesCollection = new SeriesCollection();
            periodController = new PeriodController();
        }

        private void CreateLabels(List<Period> periods, int periodAmount)
        {
            Labels = new string[periodAmount];

            for (int i = 0; i < periodAmount; i++)
            {
                Labels[i] = periodController.GetDisplayTime(periods[i]);
            }
        }

        private ChartValues<double> CreateTemperatureValues(List<Period> periods, int periodAmount)
        {
            var values = new ChartValues<double>();

            for (int i = 0; i < periodAmount; i++)
            {
                var period = periods[i];
                double temperature = period.Temperature;
                if (temperatureFormat == TemperatureFormat.Celsius)
                {
                    temperature = TemperatureUtility.ConvertFahrenheitToCelsuis(temperature);
                }
                values.Add(temperature);
            }

            return values;
        }

        public void UpdateHourlyChart(HourlyRequest hourlyRequest, int periodAmount)
        {
            SeriesCollection.Clear();

            var periods = hourlyRequest.Hourly.Properties.Periods;
            CreateLabels(periods, periodAmount);

            YFormatter = value => String.Format("{0:0.##}º", value);

            Title = "Temperature";

            SeriesCollection.Add(new LineSeries
            {
                Title = this.Title,
                Values = CreateTemperatureValues(periods, periodAmount),
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 15,
                LineSmoothness = 0.1
            });
        }

        public TemperatureFormat TemperatureFormat { get => temperatureFormat; }
        public string Title { get => title; set { title = value; RaisePropertyChanged(); } }
    }
}
