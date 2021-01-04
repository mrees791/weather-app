using GalaSoft.MvvmLight;
using System.Drawing;

namespace WeatherApp.ViewModel
{
    public class FontFamilyViewModel : ViewModelBase
    {
        private FontFamily fontFamily;

        public FontFamilyViewModel(FontFamily fontFamily)
        {
            this.fontFamily = fontFamily;
        }

        public string DisplayName
        {
            get
            {
                return fontFamily.Name;
            }
        }

        public FontFamily FontFamily { get => fontFamily; }
    }
}
