using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherLibrary.Models.WeatherGov;

namespace WeatherLibrary.Controllers.WeatherGov
{
    public class PeriodController
    {
        // Converts military time to standard (12 hr) time.
        public int ConvertMilitaryHourToStandard(int militaryHour)
        {
            if (militaryHour > 12) // 13-23
            {
                return militaryHour - 12;
            }
            else if (militaryHour == 12 || militaryHour == 0)
            {
                return 12;
            }
            else
            {
                return militaryHour;
            }
        }

        // Converts the end time string into a short, 12 hour format.
        // Example end time string: 2020-02-22T12:00:00-06:00
        public string GetDisplayTime(Period period)
        {
            var regexMatch = Regex.Match(period.EndTime, @"(?<=T)(\d*)");
            string militaryHourStr = regexMatch.Value;

            int militaryHour = int.Parse(militaryHourStr);
            int finalHour = ConvertMilitaryHourToStandard(militaryHour);

            if (militaryHour < 12)
            {
                return String.Format("{0}:00 AM", finalHour);
            }
            return String.Format("{0}:00 PM", finalHour);
        }
    }
}
