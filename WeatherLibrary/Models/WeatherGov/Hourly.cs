using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.WeatherGov
{
    public class Hourly
    {
        public HourlyProperties Properties { get; set; }
        public string Detail { get; set; }
        public string Status { get; set; }
    }
}
