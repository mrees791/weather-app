using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
