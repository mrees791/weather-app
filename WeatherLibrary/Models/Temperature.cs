namespace WeatherLibrary.Models
{
    /// <summary>
    /// Stores a temperature in both fahrenheit and celsius.
    /// </summary>
    public class Temperature
    {
        private double fahrenheit;
        private double celsius;

        public Temperature(double temperature, TemperatureFormat format)
        {
            TemperatureServices temperatureServices = new TemperatureServices();
            switch (format)
            {
                case TemperatureFormat.Fahrenheit:
                    fahrenheit = temperature;
                    celsius = temperatureServices.ConvertFahrenheitToCelsuis(fahrenheit);
                    break;
                case TemperatureFormat.Celsius:
                    celsius = temperature;
                    fahrenheit = temperatureServices.ConvertCelsiusToFahrenheit(celsius);
                    break;
            }
        }


        public double Fahrenheit { get => fahrenheit; }
        public double Celsius { get => celsius; }
    }
}
