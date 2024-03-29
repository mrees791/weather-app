﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Models.UI;
using WeatherLibrary.Models;

namespace WeatherApp.ViewModel
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly string baseStyleFileName = "Styles/BaseStyle.xaml";
        private AppFiles appFiles;
        private ObservableCollection<WpfSkinViewModel> skinViewModels;
        private ObservableCollection<FontFamilyViewModel> systemFontViewModels;

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

        public SettingsPageViewModel()
        {
            if (!IsInDesignMode)
            {
                appFiles = ((App)App.Current).AppFiles;
                InitializeSkins();
                CreateSkinViewModels();
                InitializeSystemFonts();
                InitializeCommands();
                LoadSettings();
            }
        }

        private void InitializeCommands()
        {
            applySettingsCommand = new RelayCommand(ApplySettings);
            defaultSettingsCommand = new RelayCommand(SetDefaultSettings);
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
        }

        private void SetDefaultSettings()
        {
            appFiles.SettingsFile.SetDefaults();
            LoadSettings();
        }

        private void InitializeSystemFonts()
        {
            systemFontViewModels = new ObservableCollection<FontFamilyViewModel>();
            var systemFonts = new InstalledFontCollection().Families;

            foreach (var font in systemFonts)
            {
                var fontVm = new FontFamilyViewModel(font);
                systemFontViewModels.Add(fontVm);

                if (font.Name == appFiles.SettingsFile.DefaultFont.Name)
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