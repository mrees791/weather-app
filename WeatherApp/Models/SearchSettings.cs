using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class SearchSettings
    {
        private string zipCode;

        public SearchSettings()
        {

        }

        public string ZipCode { get => zipCode; set => zipCode = value; }
    }
}
