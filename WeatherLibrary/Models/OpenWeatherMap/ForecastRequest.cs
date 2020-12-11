using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class ForecastRequest
    {
        private bool isValidRequest;
        private Forecast forecast;

        public ForecastRequest()
        {
        }

        public bool IsValidRequest { get => isValidRequest; set => isValidRequest = value; }
        public Forecast Forecast { get => forecast; set => forecast = value; }
    }
}
