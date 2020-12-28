using RestoreWindowPlace;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public WindowPlace WindowPlace { get; }

        public App()
        {
            this.WindowPlace = new WindowPlace("window.config");
            //this.Logger = LogManager.GetCurrentClassLogger();
            //DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.WindowPlace.Save();
            base.OnExit(e);
        }
    }
}
