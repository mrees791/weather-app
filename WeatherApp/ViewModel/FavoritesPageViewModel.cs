using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private FavoritesFile favoritesFile;

        public FavoritesPageViewModel()
        {
            favoritesBoxViewModels = new ObservableCollection<FavoritesBoxViewModel>();
        }

        public void RemoveFavoriteZipCode(string zipCode)
        {
            favoritesFile.RemoveZipCodeEntry(zipCode);

            var favoritesBoxEntry = favoritesBoxViewModels.Where(fbvm => fbvm.ZipCode == zipCode).FirstOrDefault();

            if (favoritesBoxEntry != null)
            {
                favoritesBoxViewModels.Remove(favoritesBoxEntry);
                SaveFavoritesFile();
            }
        }

        public void AddNewFavoriteZipCode(string zipCode, OneCall oneCall)
        {
            if (!favoritesFile.HasZipCodeEntry(zipCode))
            {
                favoritesFile.AddZipCodeEntry(zipCode);

                AddNewFavoritesBoxVm(zipCode, oneCall);

                SaveFavoritesFile();
            }
        }

        private void AddNewFavoritesBoxVm(string zipCode, OneCall oneCall)
        {
            var favoritesBoxVm = new FavoritesBoxViewModel(zipCode);
            favoritesBoxVm.ZipCode = zipCode;
            favoritesBoxVm.UpdateCurrentWeather();
            favoritesBoxViewModels.Add(favoritesBoxVm);
        }

        public void SaveFavoritesFile()
        {
            favoritesFile.WriteFile(AppDirectories.FavoritesFile);
        }

        public void LoadFavoritesFile(OneCall oneCall)
        {
            HasError = false;
            ErrorMessage = string.Empty;

            try
            {
                favoritesFile.ReadFile(AppDirectories.FavoritesFile);

                foreach (var zipCode in favoritesFile.ZipCodes)
                {
                    AddNewFavoritesBoxVm(zipCode, oneCall);
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = "Error loading favorites file.";
            }
        }

        public void UpdateFavoritesCurrentWeather()
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
        public FavoritesFile FavoritesFile { get => favoritesFile; set => favoritesFile = value; }
        public bool HasError { get => hasError; set { hasError = value; RaisePropertyChanged(); } }
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; RaisePropertyChanged(); } }
    }
}
