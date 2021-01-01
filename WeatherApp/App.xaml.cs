using RestoreWindowPlace;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public App()
        {
            appFiles = new AppFiles();
            windowPlace = new WindowPlace("window.config");
            LoadFiles();
            //this.Logger = LogManager.GetCurrentClassLogger();
            //DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void LoadFiles()
        {
            Directory.CreateDirectory(AppDirectories.UserDataDirectory);
            appFiles.LoadFavoritesFile();
            appFiles.LoadSettingsFile();

            /*try
            {
                appFiles.LoadFavoritesFile();
            }
            catch (Exception ex)
            {

            }
            try
            {
                appFiles.LoadSettingsFile();
            }
            catch (Exception ex)
            {

            }*/

        }

        protected override void OnExit(ExitEventArgs e)
        {
            appFiles.SaveAllFiles();
            windowPlace.Save();
            base.OnExit(e);
        }

        public WindowPlace WindowPlace { get => windowPlace; }
        public AppFiles AppFiles { get => appFiles; }
    }
}
