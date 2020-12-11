using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.WeatherGov
{
    public class HourlyRequest
    {
        private bool isValidRequest;
        private string errorMessage;
        private Hourly hourly;

        public HourlyRequest()
        {

        }

        public bool IsValidRequest { get => isValidRequest; set => isValidRequest = value; }
        public Hourly Hourly { get => hourly; set => hourly = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
    }
}
