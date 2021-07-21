using System.Configuration;
using System.IO;
using System.Reflection;
using UkiHelper;

namespace UkiRetroGameRandomizer.Configuration
{
    public static class AppData
    {
        public static Assembly Assembly { get; }
        public static string AppPath { get; }
        public static string ResourcePath { get; }
        public static string GameListPath { get; }
        public static string SoundPath { get; }
        public static string LogPath { get; }

        static AppData()
        {
            Assembly = typeof(AppData).Assembly;
            AppPath = Assembly.GetExecutablePath();
            ResourcePath = Path.Combine(AppPath, "Resources");
            GameListPath = Path.Combine(ResourcePath, "List");
            var soundSystem = ConfigurationManager.AppSettings["SoundSystem"];
            SoundPath = Path.Combine(ResourcePath, "Sounds", soundSystem);
            LogPath = Path.Combine(AppPath, "Log");
        }
    }
}