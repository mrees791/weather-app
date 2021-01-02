using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherLibrary.Models.OpenWeatherMap;
using WeatherLibrary.Models.Zippo;

namespace WeatherApp.ViewModel
{
    public class TopMenuViewModel : ViewModelBase
    {
        private ViewModelLocator vml;
        private AppFiles appFiles;
        private RelayCommand favoriteCurrentZipCommand;
        private RelayCommand searchNewZipCodeCommand;
        private RelayCommand<string> searchFavoriteZipCodeCommand;
        private RelayCommand refreshWeatherCommand;
        private RelayCommand setCurrentPageToWeatherPageCommand;
        private RelayCommand setCurrentPageToFavoritesPageCommand;
        private RelayCommand setCurrentPageToSettingsPageCommand;

        private string currentZip;
        private string zipInput;
        private string zipErrorMessage;
        private bool zipIsValid;
        private bool currentZipIsFavorited;
        private bool userCanToggleFavoriteButton;

        public TopMenuViewModel()
        {
            appFiles = ((App)App.Current).AppFiles;
            vml = App.Current.Resources["Locator"] as ViewModelLocator;

            ZipInput = GetDefaultZip();
            InitializeCommands();
            SearchZip(zipInput);
        }

        private void InitializeCommands()
        {
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
            setCurrentPageToWeatherPageCommand = new RelayCommand(() =>
            {
                vml.MainVm.SetCurrentPageToWeatherPage();
            });
            setCurrentPageToFavoritesPageCommand = new RelayCommand(() =>
            {
                vml.MainVm.SetCurrentPageToFavoritesPage();
            });
            setCurrentPageToSettingsPageCommand = new RelayCommand(() =>
            {
                vml.MainVm.SetCurrentPageToSettingsPage();
            });
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

        private void RefreshWeather()
        {
            UpdateWeatherData(currentZip);
            vml.MainVm.SetCurrentPageToWeatherPage();
        }

        private void SearchZip(string zip)
        {
            zipIsValid = ValidateZip(zip);

            if (zipIsValid)
            {
                ZipErrorMessage = string.Empty;
                UpdateWeatherData(zip);
                vml.MainVm.SetCurrentPageToWeatherPage();

                bool validWeatherRequest = !vml.WeatherPageVm.HasError;
                if (validWeatherRequest)
                {
                    currentZip = zip;
                    UserCanToggleFavoriteButton = true;
                    UpdateFavoriteButton();
                }
            }
            else
            {
                ZipErrorMessage = "You must enter a valid U.S. zip code.";
            }
        }

        private void UpdateWeatherData(string zip)
        {
            vml.WeatherPageVm.UpdateWeatherData(zip);
        }

        private void UpdateFavoriteButton()
        {
            CurrentZipIsFavorited = appFiles.FavoritesFile.HasZipCodeEntry(currentZip);
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

        public string ZipInput { get => zipInput; set { zipInput = value; RaisePropertyChanged(); } }
        public string ZipErrorMessage { get => zipErrorMessage; set { zipErrorMessage = value; RaisePropertyChanged(); } }
        public bool CurrentZipIsFavorited { get => currentZipIsFavorited; set { currentZipIsFavorited = value; RaisePropertyChanged(); } }
        public bool UserCanToggleFavoriteButton { get => userCanToggleFavoriteButton; set { userCanToggleFavoriteButton = value; RaisePropertyChanged(); } }

        public RelayCommand FavoriteCurrentZipCommand { get => favoriteCurrentZipCommand; }
        public RelayCommand SearchNewZipCodeCommand { get => searchNewZipCodeCommand; }
        public RelayCommand RefreshWeatherCommand { get => refreshWeatherCommand; }
        public RelayCommand<string> SearchFavoriteZipCodeCommand { get => searchFavoriteZipCodeCommand; }

        public RelayCommand SetCurrentPageToFavoritesPageCommand { get => setCurrentPageToFavoritesPageCommand; }
        public RelayCommand SetCurrentPageToSettingsPageCommand { get => setCurrentPageToSettingsPageCommand; }
        public RelayCommand SetCurrentPageToWeatherPageCommand { get => setCurrentPageToWeatherPageCommand; }
    }
}
