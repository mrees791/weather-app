using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.FileFormats;
using WeatherApp.Utility;

namespace WeatherApp.ViewModel
{
    public class FavoritesPageViewModel : ViewModelBase
    {
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

        public void AddNewFavoriteZipCode(string zipCode)
        {
            if (!favoritesFile.HasZipCodeEntry(zipCode))
            {
                favoritesFile.AddZipCodeEntry(zipCode);

                AddNewFavoritesBoxVm(zipCode);

                SaveFavoritesFile();
            }
        }

        private void AddNewFavoritesBoxVm(string zipCode)
        {
            var favoritesBoxVm = new FavoritesBoxViewModel();
            favoritesBoxVm.ZipCode = zipCode;
            favoritesBoxVm.UpdateCurrentWeather();
            favoritesBoxViewModels.Add(favoritesBoxVm);
        }

        public void SaveFavoritesFile()
        {
            favoritesFile.WriteFile(AppDirectories.FavoritesFile);
        }

        public void LoadFavoritesFile()
        {
            favoritesFile.ReadFile(AppDirectories.FavoritesFile);

            foreach (var zipCode in favoritesFile.ZipCodes)
            {
                AddNewFavoritesBoxVm(zipCode);
            }
        }

        public void UpdateFavoritesCurrentWeather()
        {
            foreach (var fbvm in favoritesBoxViewModels)
            {
                fbvm.UpdateCurrentWeather();
            }
        }

        public ObservableCollection<FavoritesBoxViewModel> FavoritesBoxViewModels { get => favoritesBoxViewModels; }
        public FavoritesFile FavoritesFile { get => favoritesFile; set => favoritesFile = value; }
    }
}
