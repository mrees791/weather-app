using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherApp.Models.FileFormats
{
    [XmlRoot(ElementName = "favorites")]
    public class FavoritesFile : FileBase
    {
        private XmlSerializer serializer;
        private string versionString;
        private Version version;
        private List<string> zipCodes;

        public FavoritesFile()
        {
            serializer = new XmlSerializer(typeof(FavoritesFile));
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            versionString = version.ToString();
            zipCodes = new List<string>();
        }

        public bool HasZipCodeEntry(string zipCode)
        {
            return zipCodes.Contains(zipCode);
        }

        public void AddZipCodeEntry(string zipCode)
        {
            zipCodes.Add(zipCode);
        }

        public void RemoveZipCodeEntry(string zipCode)
        {
            zipCodes.Remove(zipCode);
        }

        public void ReadFile(string path)
        {
            SetDefaults();

            using (var reader = new StreamReader(path))
            {
                var readFile = (FavoritesFile)serializer.Deserialize(reader);
                var fileVersion = readFile.Version;

                foreach (var zip in readFile.ZipCodes)
                {
                    AddZipCodeEntry(zip);
                }
            }
        }

        public void WriteFile(string path)
        {
            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, this);
            }
        }

        public void SetDefaults()
        {
            this.zipCodes.Clear();
        }

        [XmlElement(ElementName = "zip")]
        public List<string> ZipCodes { get => zipCodes; }
        public Version Version { get => version; }
        [XmlAttribute("appVersion")]
        public string VersionString
        {
            get
            {
                return versionString;
            }
            set
            {
                versionString = value;
            }
        }
    }
}
