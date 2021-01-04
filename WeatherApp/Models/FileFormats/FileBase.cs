namespace WeatherApp.Models.FileFormats
{
    public interface FileBase
    {
        void WriteFile(string path);
        void ReadFile(string path);
        void SetDefaults();
    }
}
