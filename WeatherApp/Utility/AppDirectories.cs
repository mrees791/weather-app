﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Utility
{
    public class AppDirectories
    {
        public static readonly string IconDirectory = "/WeatherApp;component/Resources/Images/";
        public static readonly string UserDataDirectory = "user-data";
        public static readonly string FavoritesFile = $"{UserDataDirectory}/favorites.xml";
        public static readonly string SettingsFile = $"{UserDataDirectory}/settings.xml";
    }
}
