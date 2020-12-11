/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WeatherApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace WeatherApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<UserSettingsViewModel>();
            SimpleIoc.Default.Register<CurrentWeatherViewModel>();
            SimpleIoc.Default.Register<WeatherPageViewModel>();
            SimpleIoc.Default.Register<FavoritesPageViewModel>();
        }

        public FavoritesPageViewModel FavoritesPageVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FavoritesPageViewModel>();
            }
        }

        public WeatherPageViewModel WeatherPageVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WeatherPageViewModel>();
            }
        }

        public CurrentWeatherViewModel CurrentWeatherVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CurrentWeatherViewModel>();
            }
        }

        public MainViewModel MainVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public UserSettingsViewModel UserSettingsVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserSettingsViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}