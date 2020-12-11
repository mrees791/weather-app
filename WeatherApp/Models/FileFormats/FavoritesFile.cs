using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models.FileFormats
{
    public class FavoritesFile : FileBase
    {
        private Version version;
        private List<string> zipCodes;

        public List<string> ZipCodes { get => zipCodes; }

        public FavoritesFile()
        {
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
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
            using (var reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                string fileVersion = ReadVersion(reader);
                this.zipCodes = ReadZipCodes(reader);
            }
        }

        public void WriteFile(string path)
        {
            using (var writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                WriteVersion(writer);
                WriteZipCodes(writer);
            }
        }

        private string ReadVersion(BinaryReader reader)
        {
            return reader.ReadString();
        }

        private void WriteVersion(BinaryWriter writer)
        {
            writer.Write(version.ToString());
        }

        private List<string> ReadZipCodes(BinaryReader reader)
        {
            List<string> zipCodes = new List<string>();

            int numberOfZips = reader.ReadInt32();

            for (int iZip = 0; iZip < numberOfZips; iZip++)
            {
                zipCodes.Add(reader.ReadString());
            }

            return zipCodes;
        }

        private void WriteZipCodes(BinaryWriter writer)
        {
            int numberOfZips = zipCodes.Count;

            writer.Write(numberOfZips);

            for (int iZip = 0; iZip < numberOfZips; iZip++)
            {
                writer.Write(zipCodes[iZip]);
            }
        }

        public void SetDefaults()
        {
            this.zipCodes.Clear();
        }
    }
}
