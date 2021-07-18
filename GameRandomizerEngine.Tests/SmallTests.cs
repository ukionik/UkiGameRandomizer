using System.Linq;
using NUnit.Framework;

namespace GameRandomizerEngine.Tests
{
    [TestFixture]
    public class SmallTests
    {
        [Test]
        public void PreviousNextGamesTest()
        {
            var games = new GameInfoCollection(Enumerable.Range(1, 10)
                .Select(x => new GameInfo(x.ToString()))
            );

            //1
            var game1 = games.Games[0];
            Assert.AreEqual("10", games.GetPreviousGame(game1).Name);
            Assert.AreEqual("9", games.GetPreviousGame2(game1).Name);
            Assert.AreEqual("2", games.GetNextGame(game1).Name);
            Assert.AreEqual("3", games.GetNextGame2(game1).Name);

            //2
            var game2 = games.Games[1];
            Assert.AreEqual("1", games.GetPreviousGame(game2).Name);
            Assert.AreEqual("10", games.GetPreviousGame2(game2).Name);
            Assert.AreEqual("3", games.GetNextGame(game2).Name);
            Assert.AreEqual("4", games.GetNextGame2(game2).Name);

            //3
            var game3 = games.Games[2];
            Assert.AreEqual("2", games.GetPreviousGame(game3).Name);
            Assert.AreEqual("1", games.GetPreviousGame2(game3).Name);
            Assert.AreEqual("4", games.GetNextGame(game3).Name);
            Assert.AreEqual("5", games.GetNextGame2(game3).Name);

            //8
            var game8 = games.Games[7];
            Assert.AreEqual("7", games.GetPreviousGame(game8).Name);
            Assert.AreEqual("6", games.GetPreviousGame2(game8).Name);
            Assert.AreEqual("9", games.GetNextGame(game8).Name);
            Assert.AreEqual("10", games.GetNextGame2(game8).Name);

            //9
            var game9 = games.Games[8];
            Assert.AreEqual("8", games.GetPreviousGame(game9).Name);
            Assert.AreEqual("7", games.GetPreviousGame2(game9).Name);
            Assert.AreEqual("10", games.GetNextGame(game9).Name);
            Assert.AreEqual("1", games.GetNextGame2(game9).Name);

            //10
            var game10 = games.Games[9];
            Assert.AreEqual("9", games.GetPreviousGame(game10).Name);
            Assert.AreEqual("8", games.GetPreviousGame2(game10).Name);
            Assert.AreEqual("1", games.GetNextGame(game10).Name);
            Assert.AreEqual("2", games.GetNextGame2(game10).Name);
        }
    }
}