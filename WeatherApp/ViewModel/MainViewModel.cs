using AutoUpdaterDotNET;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Device.Location;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WeatherApp.Models;
using WeatherApp.Models.FileFormats;
using WeatherApp.Utility;
using WeatherLibrary.Models;
using WeatherLibrary.Models.OpenWeatherMap;
using WeatherLibrary.Models.Zippo;

namespace WeatherApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        private ViewModelLocator vml;
        private string windowTitle;
        private AppFiles appFiles;

        private ViewModelBase selectedViewModel;
        private HourlyChartViewModel hourlyChartFahrenheitVm;
        private HourlyChartViewModel hourlyChartCelsiusVm;
        private WeatherPageViewModel weatherPageVm;

        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            appFiles = ((App)App.Current).AppFiles;

            vml = App.Current.Resources["Locator"] as ViewModelLocator;
            weatherPageVm = vml.WeatherPageVm;
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            WindowTitle = string.Format("Weather v{0}.{1}", version.Major, version.Minor);

            hourlyChartFahrenheitVm = new HourlyChartViewModel(TemperatureFormat.Fahrenheit);
            hourlyChartCelsiusVm = new HourlyChartViewModel(TemperatureFormat.Celsius);

            SetCurrentPageToWeatherPage();
        }

        private void InitializeUpdater()
        {
            new Task(() =>
            {
                StartApplicationUpdater();
            }).Start();
        }


        private void InitializeCommands()
        {
        }

        private void StartApplicationUpdater()
        {
            UpdaterUtility.CheckForUpdates();
        }

        public void SetCurrentPageToWeatherPage()
        {
            SelectedViewModel = vml.WeatherPageVm;
        }

        public void SetCurrentPageToFavoritesPage()
        {
            SelectedViewModel = vml.FavoritesPageVm;
        }

        public void SetCurrentPageToSettingsPage()
        {
            SelectedViewModel = vml.UserSettingsVm;
        }

        public string WindowTitle { get => windowTitle; set { windowTitle = value; RaisePropertyChanged(); } }

        public HourlyChartViewModel HourlyChartFahrenheitVm { get => hourlyChartFahrenheitVm; }
        public HourlyChartViewModel HourlyChartCelsiusVm { get => hourlyChartCelsiusVm; }
        public ViewModelBase SelectedViewModel { get => selectedViewModel; private set { selectedViewModel = value; RaisePropertyChanged(); } }
    }
}