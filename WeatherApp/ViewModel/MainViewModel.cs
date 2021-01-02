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

        // SelectedViewModel would be used in the DataTemplates of the ActiveViewPanel
        // to determine the active view. This has too many minor side effects
        // so we are simply using three booleans as a temporary solution.
        // private ViewModelBase selectedViewModel;

        // Determines the active page in the ActiveViewPanel.
        private bool showWeatherPage;
        private bool showFavoritesPage;
        private bool showSettingsPage;

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
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            WindowTitle = string.Format("Weather v{0}.{1}", version.Major, version.Minor);

            SetCurrentPageToWeatherPage();
        }

        private void InitializeUpdater()
        {
            new Task(() =>
            {
                StartApplicationUpdater();
            }).Start();
        }

        private void StartApplicationUpdater()
        {
            UpdaterUtility.CheckForUpdates();
        }

        public void SetCurrentPageToWeatherPage()
        {
            ShowWeatherPage = true;
            ShowFavoritesPage = false;
            ShowSettingsPage = false;
        }

        public void SetCurrentPageToFavoritesPage()
        {
            ShowWeatherPage = false;
            ShowFavoritesPage = true;
            ShowSettingsPage = false;
        }

        public void SetCurrentPageToSettingsPage()
        {
            ShowWeatherPage = false;
            ShowFavoritesPage = false;
            ShowSettingsPage = true;
        }

        public string WindowTitle { get => windowTitle; private set { windowTitle = value; RaisePropertyChanged(); } }

        public bool ShowWeatherPage { get => showWeatherPage; private set { showWeatherPage = value; RaisePropertyChanged(); } }
        public bool ShowFavoritesPage { get => showFavoritesPage; private set { showFavoritesPage = value; RaisePropertyChanged(); } }
        public bool ShowSettingsPage { get => showSettingsPage; private set { showSettingsPage = value; RaisePropertyChanged(); } }
    }
}