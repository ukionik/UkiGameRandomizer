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