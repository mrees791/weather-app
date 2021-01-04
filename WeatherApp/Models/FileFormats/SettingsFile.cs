using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
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
        private FontFamily defaultFont;
        private FontFamily[] systemFonts;

        public SettingsFile()
        {
            systemFonts = new InstalledFontCollection().Families;
            defaultFont = LoadDefaultFontFamily();
            SetDefaults();
        }

        public override void SetDefaults()
        {
            activeTemperatureFormat = TemperatureFormat.Fahrenheit;
            activeSkinName = "Light Blue";
            activeFontName = defaultFont.Name;
        }

        private FontFamily LoadDefaultFontFamily()
        {
            var segoeUi = systemFonts.Where(f => f.Name == "Segoe UI").FirstOrDefault();
            if (segoeUi != null)
            {
                return segoeUi;
            }

            return System.Drawing.SystemFonts.DefaultFont.FontFamily;
        }

        [XmlElement(ElementName = "temperatureFormat")]
        public TemperatureFormat ActiveTemperatureFormat { get => activeTemperatureFormat; set => activeTemperatureFormat = value; }
        [XmlElement(ElementName = "skin")]
        public string ActiveSkinName { get => activeSkinName; set => activeSkinName = value; }
        [XmlElement(ElementName = "font")]
        public string ActiveFontName { get => activeFontName; set => activeFontName = value; }
        [XmlIgnore]
        public FontFamily DefaultFont { get => defaultFont; }
    }
}
