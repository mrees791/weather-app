using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherApp.Models.FileFormats
{
    public abstract class XmlFileBase
    {
        protected string versionString;
        protected Version version;

        public XmlFileBase()
        {
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            versionString = version.ToString();
        }

        public Version Version { get => version; }
        [XmlAttribute("appVersion")]
        public string VersionString
        {
            get
            {
                return versionString;
            }
            set
            {
                versionString = value;
            }
        }

        public abstract void SetDefaults();
    }
}
