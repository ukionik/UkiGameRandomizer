using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameRandomizerEngine;
using Newtonsoft.Json;
using UkiRetroGameRandomizer.Configuration;
using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Util
{
    public static class PlatformGamesUtil
    {
        public static IEnumerable<GameInfo> FindGames(Platform platform, string letter = null)
        {
            var fileName = Path.Combine(AppData.GameListPath, platform.FileName);
            return File.ReadAllLines(fileName)
                .Select(x => new GameInfo(x))
                .Where(x => string.IsNullOrEmpty(letter) || x.Name.StartsWith(letter.ToUpper()))
                .Select(x => x);
        }

        public static bool HasDescription(Platform platform)
        {
            var fileName = Path.Combine(AppData.GameListPath, $"{platform.FileName.Replace(".txt", "")}_description.txt");
            return File.Exists(fileName);
        }

        public static string FindDescription(Platform platform, string gameName)
        {
            var fileName = Path.Combine(AppData.GameListPath, $"{platform.FileName.Replace(".txt", "")}_description.txt");
            var gamesDescriptionDict = File.ReadLines(fileName)
                .Select(x => new {Name = x.Split(';')[0], Description = x.Split(';')[1]})
                .ToDictionary(x => x.Name, x => x.Description);
            gamesDescriptionDict.TryGetValue(gameName, out var description);
            return description ?? "";
        }
    }
}