using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models.FileFormats
{
    public interface FileBase
    {
        void WriteFile(string path);
        void ReadFile(string path);
        void SetDefaults();
    }
}
