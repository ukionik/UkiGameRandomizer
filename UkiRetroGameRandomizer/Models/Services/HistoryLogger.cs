using System;
using System.IO;
using UkiRetroGameRandomizer.Configuration;

namespace UkiRetroGameRandomizer.Models.Services
{
    public static class HistoryLogger
    {
        private static readonly string Path;

        static HistoryLogger()
        {
            Path = System.IO.Path.Combine(AppData.LogPath, "history.log");


            if (!Directory.Exists(AppData.LogPath))
                Directory.CreateDirectory(AppData.LogPath);
        }

        public static void Log(string platformName, string platformCaption, string result)
        {
            File.AppendAllText(Path,
                $"{DateTime.Now:dd.MM.yyyy HH:mm:ss};{platformName};{platformCaption};{result}{Environment.NewLine}");
        }
    }
}