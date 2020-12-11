using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.Zippo
{
    public class ZippoRequest
    {
        private bool isValidRequest;
        private string errorMessage;
        private ZippoDetails details;

        public ZippoRequest()
        {

        }

        public bool IsValidRequest { get => isValidRequest; set => isValidRequest = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public ZippoDetails Details { get => details; set => details = value; }
    }
}
