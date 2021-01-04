using GalaSoft.MvvmLight;
using System;

namespace WeatherApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        private string windowTitle;

        public MainViewModel()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            WindowTitle = string.Format("Weather v{0}.{1}", version.Major, version.Minor);
        }

        // Application updater is disabled until we get a new FTP server for hosting update files.
        /*private void InitializeUpdater()
        {
            new Task(() =>
            {
                StartApplicationUpdater();
            }).Start();
        }

        private void StartApplicationUpdater()
        {
            UpdaterUtility.CheckForUpdates();
        }*/

        public string WindowTitle { get => windowTitle; private set { windowTitle = value; RaisePropertyChanged(); } }
    }
}