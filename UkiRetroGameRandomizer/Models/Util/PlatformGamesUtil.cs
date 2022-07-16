using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameRandomizerEngine;
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
    }
}