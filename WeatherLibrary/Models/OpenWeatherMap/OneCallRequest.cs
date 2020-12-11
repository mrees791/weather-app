using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap
{
    public class OneCallRequest
    {
        private bool isValidRequest;
        private OneCall oneCall;

        public OneCallRequest()
        {

        }

        public bool IsValidRequest { get => isValidRequest; set => isValidRequest = value; }
        public OneCall OneCall { get => oneCall; set => oneCall = value; }
    }
}
