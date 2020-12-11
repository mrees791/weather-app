using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.WeatherGov
{
    public class Period
    {
        public string ShortForecast { get; set; }
        public string TemperatureUnit { get; set; }
        public int Temperature { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
