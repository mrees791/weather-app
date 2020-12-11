using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherLibrary.Controllers;
using WeatherLibrary.Controllers.WeatherGov;

namespace WeatherTests
{
    [TestClass]
    public class PeriodControllerUnitTest
    {
        private PeriodController periodController;

        public PeriodControllerUnitTest()
        {
            periodController = new PeriodController();
        }

        [TestMethod]
        public void TestAllConvertMilitaryHourToStandard()
        {
            // Tests on 24 hours
            TestConvertMilitaryHourToStandard(0, 12);
            TestConvertMilitaryHourToStandard(1, 1);
            TestConvertMilitaryHourToStandard(2, 2);
            TestConvertMilitaryHourToStandard(3, 3);
            TestConvertMilitaryHourToStandard(4, 4);
            TestConvertMilitaryHourToStandard(5, 5);
            TestConvertMilitaryHourToStandard(6, 6);
            TestConvertMilitaryHourToStandard(7, 7);
            TestConvertMilitaryHourToStandard(8, 8);
            TestConvertMilitaryHourToStandard(9, 9);
            TestConvertMilitaryHourToStandard(10, 10);
            TestConvertMilitaryHourToStandard(11, 11);
            TestConvertMilitaryHourToStandard(12, 12);
            TestConvertMilitaryHourToStandard(13, 1);
            TestConvertMilitaryHourToStandard(14, 2);
            TestConvertMilitaryHourToStandard(15, 3);
            TestConvertMilitaryHourToStandard(16, 4);
            TestConvertMilitaryHourToStandard(17, 5);
            TestConvertMilitaryHourToStandard(18, 6);
            TestConvertMilitaryHourToStandard(19, 7);
            TestConvertMilitaryHourToStandard(20, 8);
            TestConvertMilitaryHourToStandard(21, 9);
            TestConvertMilitaryHourToStandard(22, 10);
            TestConvertMilitaryHourToStandard(23, 11);
        }

        public void TestConvertMilitaryHourToStandard(int militaryTime, int expectedHour)
        {
            int convertedHour = periodController.ConvertMilitaryHourToStandard(militaryTime);

            if (convertedHour != expectedHour)
            {
                throw new Exception(String.Format("Error converting military time to standard hour. Military time: {0} Result: {1} Expected Result: {2}", militaryTime, convertedHour, expectedHour));
            }
        }
    }
}
