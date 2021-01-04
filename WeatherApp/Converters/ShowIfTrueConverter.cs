using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WeatherApp.Converters
{
    /// <summary>
    /// Shows or hides an element depending on the boolean value passed in.
    /// </summary>
    public class ShowIfTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visibility = false;
            if (value is bool)
            {
                visibility = (bool)value;
            }
            return visibility ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return visibility == Visibility.Visible;
        }
    }
}
