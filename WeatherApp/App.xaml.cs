using RestoreWindowPlace;
using System;
using System.IO;
using System.Windows;
using WeatherApp.Models;
using WeatherApp.Utility;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WindowPlace windowPlace;
        private AppFiles appFiles;
        private bool allFilesLoaded;

        public App()
        {
            appFiles = new AppFiles();
            windowPlace = new WindowPlace("window.config");
            LoadFiles();
        }

        private void LoadFiles()
        {
            allFilesLoaded = true;
            Directory.CreateDirectory(AppDirectories.UserDataDirectory);

            try
            {
                appFiles.LoadFavoritesFile();
            }
            catch (Exception ex)
            {
                allFilesLoaded = false;
                WriteErrorToLogFile($"{AppDirectories.FavoritesFile}: {ex.Message}");
                ShowErrorPrompt("An error occured while loading the favorites file. Check log.txt for more info.");
                Shutdown();
            }
            try
            {
                appFiles.LoadSettingsFile();
            }
            catch (Exception ex)
            {
                allFilesLoaded = false;
                WriteErrorToLogFile($"{AppDirectories.SettingsFile}: {ex.Message}");
                ShowErrorPrompt("An error occured while loading the settings file. Check log.txt for more info.");
                Shutdown();
            }
        }

        private void ShowErrorPrompt(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK);
        }

        private void WriteErrorToLogFile(string logMessage)
        {
            File.AppendAllText(AppDirectories.LogFile, $"{logMessage}\n");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (allFilesLoaded)
            {
                appFiles.SaveAllFiles();
            }
            windowPlace.Save();
            base.OnExit(e);
        }

        public WindowPlace WindowPlace { get => windowPlace; }
        public AppFiles AppFiles { get => appFiles; }
    }
}
