using System.IO;
using System.Xml.Serialization;
using WeatherApp.Models.FileFormats;
using WeatherApp.Utility;

namespace WeatherApp.Models
{
    /// <summary>
    /// Stores external files, including user-data files such as settings and favorites.
    /// Also includes serializers.
    /// </summary>
    public class AppFiles
    {
        private FavoritesFile favoritesFile;
        private SettingsFile settingsFile;
        private XmlSerializer favoritesSerializer;
        private XmlSerializer settingsSerializer;

        public AppFiles()
        {
            favoritesFile = new FavoritesFile();
            settingsFile = new SettingsFile();
            favoritesSerializer = new XmlSerializer(typeof(FavoritesFile));
            settingsSerializer = new XmlSerializer(typeof(SettingsFile));
        }

        public void SaveFavoritesFile()
        {
            using (var writer = new StreamWriter(AppDirectories.FavoritesFile))
            {
                favoritesSerializer.Serialize(writer, favoritesFile);
            }
        }

        public void SaveSettingsFile()
        {
            using (var writer = new StreamWriter(AppDirectories.SettingsFile))
            {
                settingsSerializer.Serialize(writer, settingsFile);
            }
        }

        public void LoadFavoritesFile()
        {
            if (!File.Exists(AppDirectories.FavoritesFile))
            {
                SaveFavoritesFile();
            }

            using (var reader = new StreamReader(AppDirectories.FavoritesFile))
            {
                favoritesFile = (FavoritesFile)favoritesSerializer.Deserialize(reader);
            }
        }

        public void LoadSettingsFile()
        {
            if (!File.Exists(AppDirectories.SettingsFile))
            {
                SaveSettingsFile();
            }

            using (var reader = new StreamReader(AppDirectories.SettingsFile))
            {
                settingsFile = (SettingsFile)settingsSerializer.Deserialize(reader);
            }
        }

        public void SaveAllFiles()
        {
            SaveFavoritesFile();
            SaveSettingsFile();
        }

        public FavoritesFile FavoritesFile { get => favoritesFile; }
        public SettingsFile SettingsFile { get => settingsFile; }
    }
}
