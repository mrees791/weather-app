using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Utility;
using WeatherLibrary.Models;

namespace WeatherApp.Models.UI
{

    public class WeatherIcon
    {
        private string iconUrl;

        public WeatherIcon(WeatherType weatherType)
        {
            this.iconUrl = GetIconUrl(weatherType);
        }

        private string GetIconUrl(WeatherType weatherType)
        {
            string iconDirectory = AppDirectories.IconDirectory;

            switch (weatherType)
            {
                case WeatherLibrary.Models.WeatherType.Clear:
                    return iconDirectory + "sun1.xaml";
                case WeatherLibrary.Models.WeatherType.Cloudy:
                    return iconDirectory + "cloud1.xaml";
                case WeatherLibrary.Models.WeatherType.Rain:
                    return iconDirectory + "rain1.xaml";
                case WeatherLibrary.Models.WeatherType.Snow:
                    return iconDirectory + "snow1.xaml";
            }

            return iconDirectory + "unknown.xaml";
        }

        public string IconUrl { get => iconUrl; }
    }
}
