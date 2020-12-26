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
            favoritesFile.ReadFile(AppDirectories.FavoritesFile);

            foreach (var zipCode in favoritesFile.ZipCodes)
            {
                AddNewFavoritesBoxVm(zipCode, oneCall);
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
