using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models
{
    /// <summary>
    /// Weather types from OpenWeatherMap
    /// https://openweathermap.org/weather-conditions
    /// </summary>
    public enum WeatherType
    {
        Thunderstorm, Drizzle, Rain, Snow,
        Mist, Smoke, Haze, Dust, Fog, Sand,
        Ash, Squall, Tornado, Clear, Cloudy,
        Unknown
    }
}
