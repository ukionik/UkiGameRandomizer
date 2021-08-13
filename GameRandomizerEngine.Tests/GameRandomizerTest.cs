using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace GameRandomizerEngine.Tests
{
    [TestFixture]
    public class GameRandomizerTest
    {
        private IEnumerable<GameInfo> _games = new List<GameInfo>
        {
            new GameInfo("Metal Mech"),
            new GameInfo("Dracula"),
            new GameInfo("Jurassic Park"),
            new GameInfo("Battletoads"),
            new GameInfo("R.C. Pro Am"),
            new GameInfo("Contra"),
            new GameInfo("Darkwing Duck"),
            new GameInfo("Super C"),
            new GameInfo("Titan"),
            new GameInfo("Wally Bear")
        };

        [Test]
        public void Test()
        {
            var maxTime = 10 * 1000;
            var currentTime = 0;
            var tick = 100;

            var randomizer = new GameRandomizer(_games, maxTime, DateTime.Now.Millisecond);

            while (currentTime < maxTime)
            {
                var gameInfo = randomizer.GetGame(currentTime);
                Console.WriteLine(gameInfo.Name);
                currentTime += tick;
            }
        }

        [Test]
        public void TestNTimes()
        {
            var count = 1000;

            var dict = new Dictionary<GameInfo, int>();

            var seed = DateTime.Now.Millisecond;
            for (var i = 0; i < count; i++)
            {
                var winner = GetWinner(_games, seed++);

                if (dict.ContainsKey(winner))
                {
                    dict[winner]++;
                }
                else
                {
                    dict[winner] = 1;
                }
            }

            foreach (var winner in dict)
            {
                Console.WriteLine($"Winner: {winner.Key} {winner.Value}");
            }

            Console.WriteLine($"Total: {dict.Sum(x => x.Value)}");
        }

        [Test]
        public void TestNTimesRealGames()
        {
            var games = File.ReadAllLines(@"d:\Temp\UkiRetroGameRandomizer\nes.txt")
                .Select(x => new GameInfo(x))
                .ToList();

            var count = 100;

            var dict = new Dictionary<GameInfo, int>();

            var seed = DateTime.Now.Millisecond;
            for (var i = 0; i < count; i++)
            {
                var winner = GetWinner(games, seed++);

                if (dict.ContainsKey(winner))
                {
                    dict[winner]++;
                }
                else
                {
                    dict[winner] = 1;
                }
            }

            foreach (var winner in dict.OrderByDescending(x => x.Value).Where(x => x.Value > 1))
            {
                Console.WriteLine($"Winner: {winner.Key} {winner.Value}");
            }

            Console.WriteLine($"Total: {dict.Sum(x => x.Value)}");
            Console.WriteLine($"Total Games: {dict.Count}");
        }

        [Test]
        public void TempTest()
        {
            var random = new Random(DateTime.Now.Millisecond);

            var dict = new Dictionary<int, int>();

            var attempt = 0;
            while (attempt < 176)
            {
                var newNumber = random.Next(1, 1201);

                if (dict.ContainsKey(newNumber))
                {
                    var value = dict[newNumber];
                    dict[newNumber] = value + 1;
                }
                else
                {
                    dict[newNumber] = 1;
                }

                attempt++;
            }

            foreach (var keyValuePair in dict.Where(x => x.Value > 1)
                .OrderBy(x => x.Key))
            {
                Console.WriteLine($"Номер: {keyValuePair.Key} Кол-во: {keyValuePair.Value}");
            }
        }

        [Test]
        public void Temp2Test()
        {
            var games = File.ReadAllLines(@"d:\Temp\temp.txt")
                .OrderBy(x => x)
                .ToList();

            var dict = new Dictionary<string, int>();
            foreach (var game in games)
            {
                if (dict.ContainsKey(game))
                {
                    var val = dict[game];
                    dict[game] = val + 1;
                }
                else
                {
                    dict[game] = 1;
                }
            }
            
            foreach (var keyValuePair in dict.Where(x => x.Value > 1)
                .OrderBy(x => x.Key))
            {
                Console.WriteLine($"Игра: {keyValuePair.Key} Кол-во: {keyValuePair.Value}");
            }
                
        }

        private GameInfo GetWinner(IEnumerable<GameInfo> games, int seed)
        {
            var maxTime = 10 * 1000;
            var currentTime = 0;
            var tick = 100;

            var randomizer = new GameRandomizer(games, maxTime, seed);
            GameInfo gameInfo = null;

            while (currentTime < maxTime)
            {
                gameInfo = randomizer.GetGame(currentTime);
                currentTime += tick;
            }

            return gameInfo;
        }
    }
}