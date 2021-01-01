using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using WeatherApp.Models;
using WeatherApp.Models.FileFormats;
using WeatherApp.Models.UI;
using WeatherApp.Utility;
using WeatherLibrary.Models;

namespace WeatherApp.ViewModel
{
    public class UserSettingsViewModel : ViewModelBase
    {
        private readonly string baseStyleFileName = "Styles/BaseStyle.xaml";
        private AppFiles appFiles;
        private ObservableCollection<WpfSkinViewModel> skinViewModels;
        private ObservableCollection<FontFamilyViewModel> systemFontViewModels;

        private WpfSkinViewModel defaultSkinVm;
        private WpfSkinViewModel selectedSkinVm;
        private WpfSkinViewModel activeSkinVm;

        private FontFamilyViewModel defaultFontVm;
        private FontFamilyViewModel selectedFontVm;
        private FontFamilyViewModel activeFontVm;

        private TemperatureFormat activeTemperatureFormat;

        private ICommand applySettingsCommand;
        public ICommand ApplySettingsCommand { get => applySettingsCommand; }

        private ICommand defaultSettingsCommand;
        public ICommand DefaultSettingsCommand { get => defaultSettingsCommand; }

        private ICommand setFahrenheitTemperatureCommand;
        public ICommand SetFahrenheitTemperatureCommand { get => setFahrenheitTemperatureCommand; }

        private ICommand setCelsiusTemperatureCommand;
        public ICommand SetCelsiusTemperatureCommand { get => setCelsiusTemperatureCommand; }

        private WpfSkin[] skins;

        public UserSettingsViewModel()
        {
            appFiles = ((App)App.Current).AppFiles;

            InitializeSkins();
            CreateSkinViewModels();
            InitializeSystemFonts();

            LoadSettings();

            applySettingsCommand = new RelayCommand(() =>
            {
                ApplySettings();
            });
            defaultSettingsCommand = new RelayCommand(() =>
            {
                SetDefaultSettings();
            });
            setFahrenheitTemperatureCommand = new RelayCommand(() =>
            {
                ActiveTemperatureFormat = TemperatureFormat.Fahrenheit;
            });
            setCelsiusTemperatureCommand = new RelayCommand(() =>
            {
                ActiveTemperatureFormat = TemperatureFormat.Celsius;
            });
        }

        private void InitializeSkins()
        {
            skins = new WpfSkin[]
            {
                new Models.UI.WpfSkin("Light Blue", "Skins/Skin1.xaml"),
                new Models.UI.WpfSkin("Light Red", "Skins/Skin2.xaml"),
                new Models.UI.WpfSkin("Light Gray", "Skins/Skin3.xaml")
            };
        }

        private void LoadSettings()
        {
            SelectedFontVm = systemFontViewModels.Where(vm => vm.FontFamily.Name == appFiles.SettingsFile.ActiveFontName).FirstOrDefault();
            SelectedSkinVm = skinViewModels.Where(vm => vm.WpfSkin.DisplayName == appFiles.SettingsFile.ActiveSkinName).FirstOrDefault();
            ActiveTemperatureFormat = appFiles.SettingsFile.ActiveTemperatureFormat;
            ApplySettings();
            /*if (!File.Exists(AppDirectories.SettingsFile))
            {
                SetDefaultSettings();
            }
            else
            {
                try
                {
                    settingsFile.ReadFile(AppDirectories.SettingsFile);
                    SelectedFontVm = systemFontViewModels.Where(vm => vm.FontFamily.Name == settingsFile.ActiveFontFamily.Name).FirstOrDefault();
                    SelectedSkinVm = skinViewModels.Where(vm => vm.WpfSkin == settingsFile.ActiveSkin).FirstOrDefault();
                    ActiveTemperatureFormat = settingsFile.ActiveTemperatureFormat;
                    ApplySettings();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    SetDefaultSettings();
                }
            }*/
        }

        private void SetDefaultSettings()
        {
            appFiles.SettingsFile.SetDefaults();
            ApplySettings();
            /*SelectedFontVm = systemFontViewModels.Where(vm => vm.FontFamily.Name == settingsFile.ActiveFontFamily.Name).FirstOrDefault();
            SelectedSkinVm = skinViewModels.Where(vm => vm.WpfSkin.DisplayName == settingsFile.ActiveSkinName).FirstOrDefault();
            ActiveTemperatureFormat = settingsFile.ActiveTemperatureFormat;
            ApplySettings();*/
        }

        private void InitializeSystemFonts()
        {
            systemFontViewModels = new ObservableCollection<FontFamilyViewModel>();
            var systemFonts = new InstalledFontCollection().Families;

            foreach (var font in systemFonts)
            {
                var fontVm = new FontFamilyViewModel(font);
                systemFontViewModels.Add(fontVm);

                if (font.Name == System.Drawing.SystemFonts.DefaultFont.FontFamily.Name)
                {
                    defaultFontVm = fontVm;
                }
            }
        }

        private void ApplySettings()
        {
            ActiveFontVm = SelectedFontVm;
            ActiveSkinVm = SelectedSkinVm;
        }

        private void CreateSkinViewModels()
        {
            skinViewModels = new ObservableCollection<WpfSkinViewModel>();

            foreach (var skin in skins)
            {
                skinViewModels.Add(new WpfSkinViewModel(skin));
            }
        }

        public ObservableCollection<WpfSkinViewModel> SkinViewModels { get => skinViewModels; }

        public WpfSkinViewModel SelectedSkinVm
        {
            get => selectedSkinVm;
            set
            {
                selectedSkinVm = value;
                RaisePropertyChanged();
            }
        }

        public WpfSkinViewModel ActiveSkinVm
        {
            get => activeSkinVm;
            set
            {
                activeSkinVm = value;
                appFiles.SettingsFile.ActiveSkinName = activeSkinVm.DisplayName;
                //settingsFile.ActiveSkin = activeSkinVm.WpfSkin;
                UpdateSkinResources();
                RaisePropertyChanged();
            }
        }

        private void UpdateSkinResources()
        {
            var currentResources = Application.Current.Resources;
            currentResources.MergedDictionaries.Clear();

            var baseStyle = new ResourceDictionary();
            baseStyle.Source = new Uri(baseStyleFileName, UriKind.Relative);
            currentResources.MergedDictionaries.Add(baseStyle);
            var skinDict = new ResourceDictionary();
            skinDict.Source = new Uri(activeSkinVm.WpfSkin.FileName, UriKind.Relative);
            currentResources.MergedDictionaries.Add(skinDict);
        }

        public ObservableCollection<FontFamilyViewModel> SystemFontViewModels { get => systemFontViewModels; }
        public FontFamilyViewModel SelectedFontVm { get => selectedFontVm; set { selectedFontVm = value; RaisePropertyChanged(); } }
        public FontFamilyViewModel ActiveFontVm { get => activeFontVm; set { activeFontVm = value; appFiles.SettingsFile.ActiveFontName = activeFontVm.FontFamily.Name; RaisePropertyChanged(); } }
        public TemperatureFormat ActiveTemperatureFormat { get => activeTemperatureFormat; set { activeTemperatureFormat = value; appFiles.SettingsFile.ActiveTemperatureFormat = value; RaisePropertyChanged(); } }
    }
}