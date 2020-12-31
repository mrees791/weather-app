using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;
using WeatherApp.Models.UI;
using WeatherApp.ViewModel;
using WeatherLibrary.Models;

namespace WeatherApp.Models.FileFormats
{
    [XmlRoot(ElementName = "settings")]
    public class SettingsFile : FileBase
    {
        private XmlSerializer serializer;
        private string versionString;
        private Version version;
        private TemperatureFormat defaultTemperatureFormat;
        private TemperatureFormat activeTemperatureFormat;
        private WpfSkin activeSkin;
        private System.Drawing.FontFamily activeFontFamily;
        private System.Drawing.FontFamily defaultFont;

        private WpfSkin[] skins;
        private System.Drawing.FontFamily[] systemFonts;

        public SettingsFile()
        {
            serializer = new XmlSerializer(typeof(FavoritesFile));
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            versionString = version.ToString();

            defaultTemperatureFormat = TemperatureFormat.Fahrenheit;
            activeTemperatureFormat = defaultTemperatureFormat;
            defaultFont = System.Drawing.SystemFonts.DefaultFont.FontFamily;

            InitializeSystemFonts();
            InitializeSkins();
        }

        private void InitializeSystemFonts()
        {
            this.systemFonts = new InstalledFontCollection().Families;
        }

        private void InitializeSkins()
        {
            skins = new WpfSkin[]
            {
                new Models.UI.WpfSkin("Light Blue", "Skins/Skin1.xaml"),
                new Models.UI.WpfSkin("Light Red", "Skins/Skin2.xaml"),
                new Models.UI.WpfSkin("Light Gray", "Skins/Skin3.xaml")
            };
        }

        public void ReadFile(string path)
        {
            /*using (var reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                string fileVersion = ReadVersion(reader);
                this.activeTemperatureFormat = ReadTemperatureFormat(reader);
                this.activeSkin = ReadActiveSkin(reader);
                this.activeFontFamily = ReadActiveFontFamily(reader);
            }*/
        }

        private WpfSkin ReadActiveSkin(BinaryReader reader)
        {
            string displayName = reader.ReadString();
            WpfSkin skin = skins.Where(s => s.DisplayName == displayName).FirstOrDefault();

            if (skin == null)
            {
                throw new Exception(string.Format("{0} is not a valid skin.", displayName));
            }
            return skin;
        }

        private TemperatureFormat ReadTemperatureFormat(BinaryReader reader)
        {
            string tempString = reader.ReadString();
            switch (tempString)
            {
                case "Fahrenheit":
                    return TemperatureFormat.Fahrenheit;
                case "Celsius":
                    return TemperatureFormat.Celsius;
            }
            throw new Exception(string.Format("{0} is an invalid temperature format.", tempString));
        }

        private System.Drawing.FontFamily ReadActiveFontFamily(BinaryReader reader)
        {
            string fontFamilyName = reader.ReadString();
            System.Drawing.FontFamily font = systemFonts.Where(f => f.Name == fontFamilyName).FirstOrDefault();
            if (font == null)
            {
                throw new Exception(string.Format("{0} is not a valid font family.", fontFamilyName));
            }
            return font;
        }

        private string ReadVersion(BinaryReader reader)
        {
            return reader.ReadString();
        }

        public void SetDefaults()
        {
            activeTemperatureFormat = TemperatureFormat.Fahrenheit;
            activeSkin = skins[0];
            activeFontFamily = defaultFont;
        }

        public void WriteFile(string path)
        {
            /*using (var writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                WriteVersion(writer);
                WriteTemperatureFormat(writer);
                WriteActiveSkin(writer);
                WriteActiveFontFamily(writer);
            }*/
        }
        
        private void WriteActiveFontFamily(BinaryWriter writer)
        {
            writer.Write(activeFontFamily.Name);
        }

        private void WriteActiveSkin(BinaryWriter writer)
        {
            writer.Write(activeSkin.DisplayName);
        }

        private void WriteVersion(BinaryWriter writer)
        {
            writer.Write(version.ToString());
        }

        private void WriteTemperatureFormat(BinaryWriter writer)
        {
            writer.Write(activeTemperatureFormat.ToString());
        }

        public WpfSkin ActiveSkin { get => activeSkin; set => activeSkin = value; }

        public TemperatureFormat TemperatureFormat { get => activeTemperatureFormat; set => activeTemperatureFormat = value; }
        public WpfSkin[] Skins { get => skins; }
        public System.Drawing.FontFamily[] SystemFonts { get => systemFonts; }
        public System.Drawing.FontFamily DefaultFont { get => defaultFont; }
        public System.Drawing.FontFamily ActiveFontFamily { get => activeFontFamily; set => activeFontFamily = value; }
        public TemperatureFormat DefaultTemperatureFormat { get => defaultTemperatureFormat; }
    }
}
