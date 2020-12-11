using AutoUpdaterDotNET;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using System;
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
using WeatherLibrary.Controllers;
using WeatherLibrary.Controllers.OpenWeatherMap;
using WeatherLibrary.Controllers.WeatherGov;
using WeatherLibrary.Controllers.Zippo;
using WeatherLibrary.Models;
using WeatherLibrary.Models.OpenWeatherMap;
using WeatherLibrary.Models.WeatherGov;
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
        private string errorMessage;
        private bool hasErrorMessage;
        private bool userCanSearch;

        private SearchSettings searchSettings;

        private ICommand setCurrentPageToWeatherPageCommand;
        private ICommand setCurrentPageToFavoritesPageCommand;
        private ICommand setCurrentPageToSettingsPageCommand;
        private ICommand favoriteCurrentZipCommand;
        private ICommand searchZipCodeCommand;
        private ICommand searchNewZipCodeCommand;
        private bool showWeatherPage;
        private bool showFavoritesPage;
        private bool showSettingsPage;

        private string zipInput;

        private bool zipIsValid;
        private string zipErrorMessage;

        private OneCallRequest oneCallRequest;
        private ForecastRequest forecastRequest;
        private ZippoRequest zipRequest;
        private HourlyRequest hourlyRequest;

        private ZippoController zipController;
        private OneCallController oneCallController;
        private ForecastController forecastController;
        private HourlyController hourlyController;
        private PeriodController periodController;

        private int periodAmount = 7;
        private DateTime requestTime;

        private HourlyChartViewModel hourlyChartFahrenheitVm;
        private HourlyChartViewModel hourlyChartCelsiusVm;
        private CurrentWeatherViewModel currentWeatherVm;
        private WeatherPageViewModel weatherPageVm;

        private bool currentZipIsFavorited;

        // Files
        private FavoritesFile favoritesFile;

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
            
            vml = App.Current.Resources["Locator"] as ViewModelLocator;

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            WindowTitle = string.Format("Weather v{0}.{1}", version.Major, version.Minor);

            ErrorMessage = "";
            UserCanSearch = true;

            oneCallRequest = new OneCallRequest();
            oneCallController = new OneCallController();
            zipRequest = new ZippoRequest();
            hourlyRequest = new HourlyRequest();
            zipController = new ZippoController();
            hourlyController = new HourlyController();
            periodController = new PeriodController();
            forecastRequest = new ForecastRequest();
            forecastController = new ForecastController();

            searchSettings = new SearchSettings();
            ZipInput = "43762";

            SetCurrentPageToWeatherPage();
            currentWeatherVm = vml.CurrentWeatherVm;
            weatherPageVm = vml.WeatherPageVm;
            hourlyChartFahrenheitVm = new HourlyChartViewModel(TemperatureFormat.Fahrenheit);
            hourlyChartCelsiusVm = new HourlyChartViewModel(TemperatureFormat.Celsius);

            InitializeFiles();

            InitializeCommands();
            SearchCurrentZip();

            InitializeUpdater();
        }

        private void InitializeUpdater()
        {
            /*DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += delegate
            {
                StartApplicationUpdater();
            };
            timer.Start();*/
            new Task(() =>
            {
                StartApplicationUpdater();
            }).Start();
        }

        private void SearchCurrentZip()
        {
            ValidateZip();

            if (zipIsValid)
            {
                UpdateFavoriteButton();
                UpdateWeatherData();
                SetCurrentPageToWeatherPage();
            }
        }

        private void InitializeFiles()
        {
            InitializeFavoritesFile();
        }

        private void InitializeFavoritesFile()
        {
            favoritesFile = new FavoritesFile();
            vml.FavoritesPageVm.FavoritesFile = favoritesFile;

            if (File.Exists(AppDirectories.FavoritesFile))
            {
                vml.FavoritesPageVm.LoadFavoritesFile();
            }
            else
            {
                vml.FavoritesPageVm.SaveFavoritesFile();
            }
        }

        private void UpdateWeatherData()
        {
            requestTime = DateTime.Now;
            zipController.RequestZipCodeDetails(ZipInput, zipRequest);
            if (zipRequest.IsValidRequest)
            {
                Place place = zipRequest.Details.Places[0];
                hourlyController.RequestHourly(place.Latitude, place.Longitude, hourlyRequest);
                if (hourlyRequest.IsValidRequest)
                {
                    hourlyChartFahrenheitVm.UpdateHourlyChart(hourlyRequest, periodAmount);
                    hourlyChartCelsiusVm.UpdateHourlyChart(hourlyRequest, periodAmount);
                    forecastController.RequestForecast(place.Latitude, place.Longitude, forecastRequest);
                    UpdateCurrentWeatherPanelVm();
                    HasErrorMessage = false;
                    UserCanSearch = true;
                }
                else
                {
                    ErrorMessage = hourlyRequest.ErrorMessage;
                    HasErrorMessage = true;
                    UserCanSearch = false;
                }
                oneCallController.RequestOneCall(place.Latitude, place.Longitude, oneCallRequest);
                if (oneCallRequest.IsValidRequest)
                {
                    UpdateWeatherPageVm();
                }
                else
                {
                    HasErrorMessage = true;
                    UserCanSearch = false;
                }
            }
            else
            {
                ErrorMessage = zipRequest.ErrorMessage;
                HasErrorMessage = true;
                UserCanSearch = false;
            }
        }

        private void UpdateFavoriteButton()
        {
            CurrentZipIsFavorited = favoritesFile.HasZipCodeEntry(zipInput);
        }

        private void UpdateWeatherPageVm()
        {
            weatherPageVm.UpdateDailyForecast(oneCallRequest);
        }

        private void UpdateCurrentWeatherPanelVm()
        {
            currentWeatherVm.UpdateCurrentWeather(requestTime, hourlyRequest, zipRequest);
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
            searchZipCodeCommand = new RelayCommand(() =>
            {
                SearchCurrentZip();
            });
            favoriteCurrentZipCommand = new RelayCommand(() =>
            {
                ToggleCurrentZipFavorite();
            });
            searchNewZipCodeCommand = new RelayCommand<string>((zip) =>
            {
                this.ZipInput = zip;
                SearchCurrentZip();
            });
        }

        private void ValidateZip()
        {
            var regexMatch = Regex.Match(zipInput, @"^\d{5}$");

            if (regexMatch.Success)
            {
                zipIsValid = true;
                ZipErrorMessage = "";
            }
            else
            {
                zipIsValid = false;
                ZipErrorMessage = "You must enter a valid U.S. zip code.";
            }
        }

        private void ToggleCurrentZipFavorite()
        {
            ValidateZip();

            if (zipIsValid)
            {
                if (!currentZipIsFavorited)
                {
                    vml.FavoritesPageVm.AddNewFavoriteZipCode(zipInput);
                }
                else
                {
                    vml.FavoritesPageVm.RemoveFavoriteZipCode(zipInput);
                }
                UpdateFavoriteButton();
            }
        }

        private void StartApplicationUpdater()
        {
            UpdaterUtility.CheckForUpdates();
        }

        private void SetCurrentPageToWeatherPage()
        {
            ShowFavoritesPage = false;
            ShowSettingsPage = false;
            ShowWeatherPage = true;
        }

        private void SetCurrentPageToFavoritesPage()
        {
            ShowSettingsPage = false;
            ShowWeatherPage = false;
            ShowFavoritesPage = true;
        }

        private void SetCurrentPageToSettingsPage()
        {
            ShowWeatherPage = false;
            ShowFavoritesPage = false;
            ShowSettingsPage = true;
        }

        public bool ShowWeatherPage { get => showWeatherPage; set { showWeatherPage = value; RaisePropertyChanged(); } }
        public bool ShowFavoritesPage { get => showFavoritesPage; set { showFavoritesPage = value; RaisePropertyChanged(); } }
        public bool ShowSettingsPage { get => showSettingsPage; set { showSettingsPage = value; RaisePropertyChanged(); } }

        public ICommand SearchZipCodeCommand { get => searchZipCodeCommand; }
        public ICommand SetCurrentPageToFavoritesPageCommand { get => setCurrentPageToFavoritesPageCommand; }
        public ICommand SetCurrentPageToSettingsPageCommand { get => setCurrentPageToSettingsPageCommand; }
        public ICommand SetCurrentPageToWeatherPageCommand { get => setCurrentPageToWeatherPageCommand; }
        public string ZipInput { get => zipInput; set { searchSettings.ZipCode = value; zipInput = value; RaisePropertyChanged(); } }

        public bool CurrentZipIsFavorited { get => currentZipIsFavorited; set { currentZipIsFavorited = value; RaisePropertyChanged(); } }

        public ICommand FavoriteCurrentZipCommand { get => favoriteCurrentZipCommand; }
        public ICommand SearchNewZipCodeCommand { get => searchNewZipCodeCommand; }
        public string WindowTitle { get => windowTitle; set { windowTitle = value; RaisePropertyChanged(); } }

        public string ErrorMessage { get => errorMessage; set { errorMessage = value; RaisePropertyChanged(); } }

        public string ZipErrorMessage { get => zipErrorMessage; set { zipErrorMessage = value; RaisePropertyChanged(); } }

        public HourlyChartViewModel HourlyChartFahrenheitVm { get => hourlyChartFahrenheitVm; }
        public HourlyChartViewModel HourlyChartCelsiusVm { get => hourlyChartCelsiusVm; }
        public bool HasErrorMessage { get => hasErrorMessage; set { hasErrorMessage = value; RaisePropertyChanged(); } }

        public bool UserCanSearch { get => userCanSearch; set { userCanSearch = value; RaisePropertyChanged(); } }
    }
}