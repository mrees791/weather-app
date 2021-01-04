using System;
using System.Xml.Serialization;

namespace WeatherApp.Models.FileFormats
{
    /// <summary>
    /// An abstract class used for XML files such as the favorites and settings files.
    /// </summary>
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
