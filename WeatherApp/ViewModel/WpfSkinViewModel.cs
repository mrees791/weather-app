using GalaSoft.MvvmLight;
using WeatherApp.Models.UI;

namespace WeatherApp.ViewModel
{
    public class WpfSkinViewModel : ViewModelBase
    {
        private WpfSkin wpfSkin;

        public WpfSkinViewModel(WpfSkin wpfSkin)
        {
            this.wpfSkin = wpfSkin;
        }

        public string DisplayName
        {
            get
            {
                return wpfSkin.DisplayName;
            }
        }

        public WpfSkin WpfSkin { get => wpfSkin; }
    }
}