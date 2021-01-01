using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Utility;
using WeatherLibrary.Models;

namespace WeatherApp.Models.UI
{
    /// <summary>
    /// Gets the icon URL for different weather types.
    /// </summary>
    public class WeatherIcon
    {
        public WeatherIcon()
        {
        }

        public string GetIconUrl(WeatherType weatherType)
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
    }
}
