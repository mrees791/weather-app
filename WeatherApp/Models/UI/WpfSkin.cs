using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models.UI
{
    public class WpfSkin
    {
        private string displayName;
        private string fileName;

        public WpfSkin(string displayName, string fileName)
        {
            this.displayName = displayName;
            this.fileName = fileName;
        }

        public string DisplayName { get => displayName; }
        public string FileName { get => fileName; }
    }
}
