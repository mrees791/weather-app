﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Text.RegularExpressions;
using WeatherApp.Models;

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

        private string zipInput;
        private string zipErrorMessage;
        private bool zipIsValid;
        private bool currentZipIsFavorited;
        private bool userCanToggleFavoriteButton;
        private bool showRefreshAndFavoriteButtons;


        // SelectedViewModel would be used in the DataTemplates of the ActiveViewPanel
        // to determine the active view. However, this has too many minor side effects
        // so we are simply using three booleans as a temporary solution.
        // private ViewModelBase selectedViewModel;

        // Determines the active page in the ActiveViewPanel.
        private bool showWeatherPage;
        private bool showFavoritesPage;
        private bool showSettingsPage;

        public TopMenuViewModel()
        {
            if (!IsInDesignMode)
            {
                appFiles = ((App)App.Current).AppFiles;
                vml = App.Current.Resources["Locator"] as ViewModelLocator;

                ZipInput = GetDefaultZip();
                InitializeCommands();
                SearchZip(zipInput);
            }
            else
            {
                ZipInput = "12345";
                ZipErrorMessage = "Error message for zip-code.";
            }
        }

        private void InitializeCommands()
        {
            favoriteCurrentZipCommand = new RelayCommand(ToggleCurrentZipFavorite);
            refreshWeatherCommand = new RelayCommand(RefreshWeather);
            setCurrentPageToWeatherPageCommand = new RelayCommand(SetCurrentPageToWeatherPage);
            setCurrentPageToFavoritesPageCommand = new RelayCommand(SetCurrentPageToFavoritesPage);
            setCurrentPageToSettingsPageCommand = new RelayCommand(SetCurrentPageToSettingsPage);
            searchNewZipCodeCommand = new RelayCommand(() =>
            {
                SearchZip(ZipInput);
            });
            searchFavoriteZipCodeCommand = new RelayCommand<string>((zip) =>
            {
                ZipInput = zip;
                SearchZip(zip);
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
            if (showWeatherPage)
            {
                vml.WeatherPageVm.RefreshWeatherData();
            }
            else if (showFavoritesPage)
            {
                vml.FavoritesPageVm.RefreshFavorites();
            }
        }

        private void SearchZip(string zip)
        {
            zipIsValid = ValidateZip(zip);

            if (zipIsValid)
            {
                ZipErrorMessage = string.Empty;
                vml.WeatherPageVm.UpdateWeatherData(zip);
                SetCurrentPageToWeatherPage();

                bool validWeatherRequest = !vml.WeatherPageVm.HasError;
                if (validWeatherRequest)
                {
                    UserCanToggleFavoriteButton = true;
                    UpdateFavoriteButton();
                }
            }
            else
            {
                ZipErrorMessage = "You must enter a valid U.S. zip code.";
            }
        }

        private void UpdateFavoriteButton()
        {
            CurrentZipIsFavorited = appFiles.FavoritesFile.HasZipCodeEntry(vml.WeatherPageVm.CurrentZip);
        }

        private bool ValidateZip(string zip)
        {
            var regexMatch = Regex.Match(zip, @"^\d{5}$");

            if (regexMatch.Success)
            {
                return true;
            }

            return false;
        }

        private void ToggleCurrentZipFavorite()
        {
            if (!currentZipIsFavorited)
            {
                vml.FavoritesPageVm.AddNewFavoriteZipCode(vml.WeatherPageVm.CurrentZip);
            }
            else
            {
                vml.FavoritesPageVm.RemoveFavoriteZipCode(vml.WeatherPageVm.CurrentZip);
            }
            UpdateFavoriteButton();
        }

        public void SetCurrentPageToWeatherPage()
        {
            ShowWeatherPage = true;
            ShowFavoritesPage = false;
            ShowSettingsPage = false;
            ShowRefreshAndFavoriteButtons = true;
        }

        public void SetCurrentPageToFavoritesPage()
        {
            ShowWeatherPage = false;
            ShowFavoritesPage = true;
            ShowSettingsPage = false;
            ShowRefreshAndFavoriteButtons = true;
        }

        public void SetCurrentPageToSettingsPage()
        {
            ShowWeatherPage = false;
            ShowFavoritesPage = false;
            ShowSettingsPage = true;
            ShowRefreshAndFavoriteButtons = false;
        }


        public bool ShowWeatherPage { get => showWeatherPage; private set { showWeatherPage = value; RaisePropertyChanged(); } }
        public bool ShowFavoritesPage { get => showFavoritesPage; private set { showFavoritesPage = value; RaisePropertyChanged(); } }
        public bool ShowSettingsPage { get => showSettingsPage; private set { showSettingsPage = value; RaisePropertyChanged(); } }

        public string ZipInput { get => zipInput; set { zipInput = value; RaisePropertyChanged(); } }
        public string ZipErrorMessage { get => zipErrorMessage; set { zipErrorMessage = value; RaisePropertyChanged(); } }
        public bool CurrentZipIsFavorited { get => currentZipIsFavorited; set { currentZipIsFavorited = value; RaisePropertyChanged(); } }
        public bool UserCanToggleFavoriteButton { get => userCanToggleFavoriteButton; set { userCanToggleFavoriteButton = value; RaisePropertyChanged(); } }
        public bool ShowRefreshAndFavoriteButtons { get => showRefreshAndFavoriteButtons; set { showRefreshAndFavoriteButtons = value; RaisePropertyChanged(); } }

        public RelayCommand FavoriteCurrentZipCommand { get => favoriteCurrentZipCommand; }
        public RelayCommand SearchNewZipCodeCommand { get => searchNewZipCodeCommand; }
        public RelayCommand RefreshWeatherCommand { get => refreshWeatherCommand; }
        public RelayCommand<string> SearchFavoriteZipCodeCommand { get => searchFavoriteZipCodeCommand; }

        public RelayCommand SetCurrentPageToFavoritesPageCommand { get => setCurrentPageToFavoritesPageCommand; }
        public RelayCommand SetCurrentPageToSettingsPageCommand { get => setCurrentPageToSettingsPageCommand; }
        public RelayCommand SetCurrentPageToWeatherPageCommand { get => setCurrentPageToWeatherPageCommand; }
    }
}
