using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Models;
using Xunit;

namespace WeatherLibrary.Tests
{
    public class TemperatureServicesTests
    {
        private TemperatureServices _temperatureServices = new TemperatureServices();

        [Theory]
        [InlineData(32.0, 0.0)]
        [InlineData(0.0, -17.78)]
        [InlineData(100.0, 37.78)]
        [InlineData(-100.0, -73.33)]
        public void ConvertFahrenheitToCelsuis_ShouldWork(double fahrenheit, double expected)
        {
            double actual = _temperatureServices.ConvertFahrenheitToCelsuis(fahrenheit);
            Assert.Equal(expected, actual, 2);
        }

        [Theory]
        [InlineData(0.0, 32.0)]
        [InlineData(37.78, 100.0)]
        [InlineData(100.0, 212.0)]
        [InlineData(-100.0, -148.0)]
        public void ConvertCelsuisToFahrenheit_ShouldWork(double celsius, double expected)
        {
            double actual = _temperatureServices.ConvertCelsiusToFahrenheit(celsius);
            Assert.Equal(expected, actual, 2);
        }
    }
}
