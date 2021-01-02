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

        private RelayCommand setCurrentPageToWeatherPageCommand;
        private RelayCommand setCurrentPageToFavoritesPageCommand;
        private RelayCommand setCurrentPageToSettingsPageCommand;
        private RelayCommand favoriteCurrentZipCommand;
        private RelayCommand searchNewZipCodeCommand;
        private RelayCommand<string> searchFavoriteZipCodeCommand;
        private RelayCommand refreshWeatherCommand;

        private string currentZip;
        private string zipInput;
        private string zipErrorMessage;
        private bool zipIsValid;
        private bool currentZipIsFavorited;

        private OneCallRequest oneCallRequest;
        private ZippoRequest zipRequest;

        private HourlyChartViewModel hourlyChartFahrenheitVm;
        private HourlyChartViewModel hourlyChartCelsiusVm;
        private WeatherPageViewModel weatherPageVm;
        private ViewModelBase selectedViewModel;

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

            oneCallRequest = new OneCallRequest();
            zipRequest = new ZippoRequest();

            hourlyChartFahrenheitVm = new HourlyChartViewModel(TemperatureFormat.Fahrenheit);
            hourlyChartCelsiusVm = new HourlyChartViewModel(TemperatureFormat.Celsius);

            ZipInput = GetDefaultZip();
            SearchZip(ZipInput);

            SetCurrentPageToWeatherPage();

            InitializeCommands();
        }

        /// <summary>
        /// This is a temporary solution until we can register for zipcodeapi.com to find the user's current zip code.
        /// </summary>
        /// <returns></returns>
        private string GetDefaultZip()
        {
            if (appFiles.FavoritesFile.ZipCodes.Count > 0)
            {
                return appFiles.FavoritesFile.ZipCodes[0];
            }

            return "43701";
        }

        private void InitializeUpdater()
        {
            new Task(() =>
            {
                StartApplicationUpdater();
            }).Start();
        }

        private void RefreshWeather()
        {
            UpdateWeatherData(currentZip);
            SetCurrentPageToWeatherPage();
        }

        private void SearchZip(string zip)
        {
            zipIsValid = ValidateZip(zip);

            if (zipIsValid)
            {
                currentZip = zip;
                ZipErrorMessage = string.Empty;
                UpdateFavoriteButton();
                UpdateWeatherData(zip);
                SetCurrentPageToWeatherPage();
            }
            else
            {
                ZipErrorMessage = "You must enter a valid U.S. zip code.";
            }
        }

        private void UpdateWeatherData(string zip)
        {
            weatherPageVm.UpdateWeatherData(zip);
        }

        private void UpdateFavoriteButton()
        {
            CurrentZipIsFavorited = appFiles.FavoritesFile.HasZipCodeEntry(ZipInput);
        }

        private void InitializeCommands()
        {
            setCurrentPageToWeatherPageCommand = new RelayCommand(() =>
            {
                SetCurrentPageToWeatherPage();
            });
            setCurrentPageToFavoritesPageCommand = new RelayCommand(() =>
            {
                SetCurrentPageToFavoritesPage();
            });
            setCurrentPageToSettingsPageCommand = new RelayCommand(() =>
            {
                SetCurrentPageToSettingsPage();
            });
            favoriteCurrentZipCommand = new RelayCommand(() =>
            {
                ToggleCurrentZipFavorite();
            });
            searchNewZipCodeCommand = new RelayCommand(() =>
            {
                SearchZip(ZipInput);
            });
            searchFavoriteZipCodeCommand = new RelayCommand<string>((zip) =>
            {
                ZipInput = zip;
                SearchZip(zip);
            });
            refreshWeatherCommand = new RelayCommand(() =>
            {
                RefreshWeather();
            });
        }

        private bool ValidateZip(string zip)
        {
            if (zip != null)
            {
                var regexMatch = Regex.Match(zip, @"^\d{5}$");

                if (regexMatch.Success)
                {
                    return true;
                }
            }

            return false;
        }

        private void ToggleCurrentZipFavorite()
        {
            if (!currentZipIsFavorited)
            {
                vml.FavoritesPageVm.AddNewFavoriteZipCode(currentZip);
            }
            else
            {
                vml.FavoritesPageVm.RemoveFavoriteZipCode(currentZip);
            }
            UpdateFavoriteButton();
        }

        private void StartApplicationUpdater()
        {
            UpdaterUtility.CheckForUpdates();
        }

        private void SetCurrentPageToWeatherPage()
        {
            SelectedViewModel = vml.WeatherPageVm;
        }

        private void SetCurrentPageToFavoritesPage()
        {
            SelectedViewModel = vml.FavoritesPageVm;
        }

        private void SetCurrentPageToSettingsPage()
        {
            SelectedViewModel = vml.UserSettingsVm;
        }

        public string WindowTitle { get => windowTitle; set { windowTitle = value; RaisePropertyChanged(); } }
        public string ZipInput { get => zipInput; set { zipInput = value; RaisePropertyChanged(); } }
        public string ZipErrorMessage { get => zipErrorMessage; set { zipErrorMessage = value; RaisePropertyChanged(); } }
        public bool CurrentZipIsFavorited { get => currentZipIsFavorited; set { currentZipIsFavorited = value; RaisePropertyChanged(); } }

        public HourlyChartViewModel HourlyChartFahrenheitVm { get => hourlyChartFahrenheitVm; }
        public HourlyChartViewModel HourlyChartCelsiusVm { get => hourlyChartCelsiusVm; }
        public ViewModelBase SelectedViewModel { get => selectedViewModel; set { selectedViewModel = value; RaisePropertyChanged(); } }

        public RelayCommand FavoriteCurrentZipCommand { get => favoriteCurrentZipCommand; }
        public RelayCommand SearchNewZipCodeCommand { get => searchNewZipCodeCommand; }
        public RelayCommand RefreshWeatherCommand { get => refreshWeatherCommand; }
        public RelayCommand SetCurrentPageToFavoritesPageCommand { get => setCurrentPageToFavoritesPageCommand; }
        public RelayCommand SetCurrentPageToSettingsPageCommand { get => setCurrentPageToSettingsPageCommand; }
        public RelayCommand SetCurrentPageToWeatherPageCommand { get => setCurrentPageToWeatherPageCommand; }
        public RelayCommand<string> SearchFavoriteZipCodeCommand { get => searchFavoriteZipCodeCommand; }
    }
}