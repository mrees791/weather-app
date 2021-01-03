using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Models.FileFormats;
using WeatherApp.Utility;
using WeatherLibrary.Models.OpenWeatherMap;

namespace WeatherApp.ViewModel
{
    public class FavoritesPageViewModel : ViewModelBase
    {
        private bool hasError;
        private string errorMessage;
        private ObservableCollection<FavoritesBoxViewModel> favoritesBoxViewModels;
        private AppFiles appFiles;

        public FavoritesPageViewModel()
        {
            appFiles = ((App)App.Current).AppFiles;
            favoritesBoxViewModels = new ObservableCollection<FavoritesBoxViewModel>();

            LoadFavoritesFile();
        }

        public void RemoveFavoriteZipCode(string zipCode)
        {
            if (appFiles.FavoritesFile.RemoveZipCodeEntry(zipCode))
            {
                RemoveFavoritesBoxVm(zipCode);
            }
        }

        private void RemoveFavoritesBoxVm(string zipCode)
        {
            var favoritesBoxEntry = favoritesBoxViewModels.Where(fbvm => fbvm.ZipCode == zipCode).FirstOrDefault();

            if (favoritesBoxEntry != null)
            {
                favoritesBoxViewModels.Remove(favoritesBoxEntry);
            }
        }

        public void AddNewFavoriteZipCode(string zipCode)
        {
            if (!appFiles.FavoritesFile.HasZipCodeEntry(zipCode))
            {
                AddNewFavoritesBoxVm(zipCode);
            }
        }

        private void AddNewFavoritesBoxVm(string zipCode)
        {
            var favoritesBoxVm = new FavoritesBoxViewModel(zipCode);
            favoritesBoxVm.ZipCode = zipCode;
            favoritesBoxVm.UpdateCurrentWeather();
            favoritesBoxViewModels.Add(favoritesBoxVm);
            appFiles.FavoritesFile.AddZipCodeEntry(zipCode);
        }

        public void LoadFavoritesFile()
        {
            HasError = false;
            ErrorMessage = string.Empty;

            try
            {
                foreach (var zipCode in appFiles.FavoritesFile.ZipCodes)
                {
                    AddNewFavoritesBoxVm(zipCode);
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = "Error loading favorites file.";
            }
        }

        public void RefreshFavorites()
        {
            HasError = false;
            ErrorMessage = string.Empty;

            try
            {
                foreach (var fbvm in favoritesBoxViewModels)
                {
                    fbvm.UpdateCurrentWeather();
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = "Error updating weather.";
            }
        }

        public ObservableCollection<FavoritesBoxViewModel> FavoritesBoxViewModels { get => favoritesBoxViewModels; }
        public bool HasError { get => hasError; set { hasError = value; RaisePropertyChanged(); } }
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; RaisePropertyChanged(); } }
    }
}
