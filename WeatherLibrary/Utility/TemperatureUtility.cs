using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Utility
{
    public class TemperatureUtility
    {
        public static double ConvertFahrenheitToCelsuis(double fahrenheit)
        {
            return ((fahrenheit - 32.0) * (5.0 / 9.0));
        }

        public static double ConvertCelsiusToFahrenheit(double celsius)
        {
            return (celsius * (9.0 / 5.0) + 32.0);
        }
    }
}
