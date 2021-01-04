using System;
using System.Globalization;
using System.Windows.Data;
using WeatherApp.Utility;

namespace WeatherApp.Converters
{
    /// <summary>
    /// A converter which shows the correct favorite icon depending on a boolean value passed.
    /// If the value is true, then the favorite icon should be on. If the value is false, then the icon should be off.
    /// </summary>
    public class FavoritesStarIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconDirectory = AppDirectories.IconDirectory;

            if (value is bool)
            {
                bool boolVal = (bool)value;
                if (boolVal)
                {
                    return iconDirectory + "favorite_star_on.xaml";
                }
            }
            return iconDirectory + "favorite_star_off.xaml";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
