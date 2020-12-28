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
            //DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        /*private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An error occured. Check the log file for more details.", "Error");
            e.Handled = true;
        }*/

        protected override void OnExit(ExitEventArgs e)
        {
            this.WindowPlace.Save();
            base.OnExit(e);
        }
    }
}
