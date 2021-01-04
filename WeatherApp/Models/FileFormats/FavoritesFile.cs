using System.Collections.Generic;
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
            if (!HasZipCodeEntry(zipCode))
            {
                zipCodes.Add(zipCode);
                return true;
            }
            return false;
        }

        public bool RemoveZipCodeEntry(string zipCode)
        {
            return zipCodes.Remove(zipCode);
        }

        public override void SetDefaults()
        {
            zipCodes.Clear();
        }

        [XmlElement(ElementName = "zip")]
        public List<string> ZipCodes { get => zipCodes; }
    }
}
