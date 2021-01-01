using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WeatherLibrary.Models;

namespace WeatherApp.Models.FileFormats
{
    [XmlRoot(ElementName = "settings")]
    public class SettingsFile : XmlFileBase
    {
        private TemperatureFormat activeTemperatureFormat;
        private string activeSkinName;
        private string activeFontName;

        public SettingsFile()
        {
            SetDefaults();
        }

        public override void SetDefaults()
        {
            activeTemperatureFormat = TemperatureFormat.Fahrenheit;
            activeSkinName = "Light Blue";
            activeFontName = System.Drawing.SystemFonts.DefaultFont.FontFamily.Name;
        }

        [XmlElement(ElementName = "temperatureFormat")]
        public TemperatureFormat ActiveTemperatureFormat { get => activeTemperatureFormat; set => activeTemperatureFormat = value; }
        [XmlElement(ElementName = "skin")]
        public string ActiveSkinName { get => activeSkinName; set => activeSkinName = value; }
        [XmlElement(ElementName = "font")]
        public string ActiveFontName { get => activeFontName; set => activeFontName = value; }
    }
}
