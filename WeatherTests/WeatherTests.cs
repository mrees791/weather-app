using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherLibrary.Models;

namespace WeatherTests
{
    [TestClass]
    public class WeatherTests
    {
        [TestMethod]
        public void TestFahrenheitTemperatures()
        {
            Temperature freezing = new Temperature(32.0, TemperatureFormat.Fahrenheit);
            Temperature boiling = new Temperature(212.0, TemperatureFormat.Fahrenheit);

            Assert.AreEqual(0.0, freezing.Celsius, 0.01, "Freezing's celsius temperature should be 0.");
            Assert.AreEqual(100.0, boiling.Celsius, 0.01, "Boiling's celsius temperature should be 100.");

        }

        [TestMethod]
        public void TestCelsiusTemperatures()
        {
            Temperature freezing = new Temperature(0.0, TemperatureFormat.Celsius);
            Temperature boiling = new Temperature(100.0, TemperatureFormat.Celsius);

            Assert.AreEqual(32.0, freezing.Fahrenheit, 0.01, "Freezing's fahrenheit temperature should be 32.");
            Assert.AreEqual(212.0, boiling.Fahrenheit, 0.01, "Boiling's fahrenheit temperature should be 212.");
        }
    }
}