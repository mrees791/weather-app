using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WeatherLibrary.Models;

namespace WeatherApp.Converters
{
    // Shows the object if the temperature format passed is fahrenheit.
    public class ShowIfFahrenheitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TemperatureFormat format = (TemperatureFormat)value;

            if (format == TemperatureFormat.Fahrenheit)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return visibility == Visibility.Visible;
        }
    }
}
