namespace WeatherApp.Utility
{
    /// <summary>
    /// Stores directories to files used throughout the application.
    /// </summary>
    public class AppDirectories
    {
        public static readonly string IconDirectory = "/WeatherApp;component/Resources/Images/";
        public static readonly string UserDataDirectory = "user-data";
        public static readonly string FavoritesFile = $"{UserDataDirectory}/favorites.xml";
        public static readonly string SettingsFile = $"{UserDataDirectory}/settings.xml";
        public static readonly string LogFile = $"{UserDataDirectory}/log.txt";
    }
}
