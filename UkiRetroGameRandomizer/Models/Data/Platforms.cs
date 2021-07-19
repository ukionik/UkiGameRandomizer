using System.Collections.Generic;
using System.IO;
using System.Linq;
using UkiHelper;
using UkiRetroGameRandomizer.Configuration;

namespace UkiRetroGameRandomizer.Models.Data
{
    public static class Platforms
    {
        private static readonly List<Platform> _all = new List<Platform>();
        public static IEnumerable<Platform> All { get; } = _all;

        public static void InitPlatforms()
        {
            var fileName = Path.Combine(AppData.GameListPath, "metadata.txt");
            _all.Clear();
            _all.AddRange(File.ReadAllLines(fileName)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new Platform(x)));
            
        }
    }
}