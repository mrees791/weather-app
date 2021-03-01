using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Models.OpenWeatherMap.Exceptions
{
    public class NullApiKeyException : Exception
    {
        public NullApiKeyException()
        {
        }

        public NullApiKeyException(string message)
            : base(message)
        {
        }

        public NullApiKeyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
