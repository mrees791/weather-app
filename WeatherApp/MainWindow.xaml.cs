﻿using System.Windows;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ((App)Application.Current).WindowPlace.Register(this, "MainWindow");
        }
    }
}
