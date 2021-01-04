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
            switch (format)
            {
                case TemperatureFormat.Fahrenheit:
                    fahrenheit = temperature;
                    celsius = ConvertFahrenheitToCelsuis(fahrenheit);
                    break;
                case TemperatureFormat.Celsius:
                    celsius = temperature;
                    fahrenheit = ConvertCelsiusToFahrenheit(celsius);
                    break;
            }
        }

        private double ConvertFahrenheitToCelsuis(double fahrenheit)
        {
            return ((fahrenheit - 32.0) * (5.0 / 9.0));
        }

        private double ConvertCelsiusToFahrenheit(double celsius)
        {
            return (celsius * (9.0 / 5.0) + 32.0);
        }


        public double Fahrenheit { get => fahrenheit; }
        public double Celsius { get => celsius; }
    }
}
