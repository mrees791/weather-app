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
    public class FavoritesFile : XmlFileBase
    {
        private List<string> zipCodes;

        public FavoritesFile()
        {
            zipCodes = new List<string>();
        }

        public bool HasZipCodeEntry(string zipCode)
        {
            return zipCodes.Contains(zipCode);
        }

        public bool AddZipCodeEntry(string zipCode)
        {
            if (zipCodes.Contains(zipCode))
            {
                return false;
            }
            else
            {
                zipCodes.Add(zipCode);
                return true;
            }
        }

        public bool RemoveZipCodeEntry(string zipCode)
        {
            return zipCodes.Remove(zipCode);
        }

        /*public override void ReadFile(string path)
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

        public override void WriteFile(string path)
        {
            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, this);
            }
        }*/

        public override void SetDefaults()
        {
            zipCodes.Clear();
        }

        [XmlElement(ElementName = "zip")]
        public List<string> ZipCodes { get => zipCodes; }
    }
}
