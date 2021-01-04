using GalaSoft.MvvmLight;

namespace WeatherApp.ViewModel
{
    public class DayWeatherViewModel : ViewModelBase
    {
        private string date;
        private string temperatureCelsiusLow;
        private string temperatureCelsiusHigh;
        private string temperatureFahrenheitLow;
        private string temperatureFahrenheitHigh;
        private string description;
        private string iconUrl;

        public DayWeatherViewModel()
        {
            if (IsInDesignMode)
            {
                Date = "Jan 3";
                TemperatureCelsiusLow = "-0.51";
                TemperatureCelsiusHigh = "5.02";
                TemperatureFahrenheitLow = "31.08";
                TemperatureFahrenheitHigh = "41.04";
                Description = "Rain";
                IconUrl = "http://openweathermap.org/img/wn/10d@4x.png";
            }
        }

        public string Date { get => date; set { date = value; RaisePropertyChanged(); } }
        public string TemperatureCelsiusLow { get => temperatureCelsiusLow; set { temperatureCelsiusLow = value; RaisePropertyChanged(); } }
        public string TemperatureCelsiusHigh { get => temperatureCelsiusHigh; set { temperatureCelsiusHigh = value; RaisePropertyChanged(); } }
        public string TemperatureFahrenheitLow { get => temperatureFahrenheitLow; set { temperatureFahrenheitLow = value; RaisePropertyChanged(); } }
        public string TemperatureFahrenheitHigh { get => temperatureFahrenheitHigh; set { temperatureFahrenheitHigh = value; RaisePropertyChanged(); } }
        public string Description { get => description; set { description = value; RaisePropertyChanged(); } }
        public string IconUrl { get => iconUrl; set { iconUrl = value; RaisePropertyChanged(); } }
    }
}
