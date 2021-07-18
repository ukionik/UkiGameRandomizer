using System;
using System.Collections.Generic;
using System.Linq;

namespace GameRandomizerEngine
{
    public class GameInfoCollection
    {
        public List<GameInfo> Games { get; }

        public double Length { get; private set; }

        public GameInfoCollection(IEnumerable<GameInfo> games)
        {
            Games = games.ToList();
            Length = Games.Count;
        }

        public void Shuffle(Random r)
        {
            Games.Shuffle(r);
            Length = Games.Count;
        }

        public GameInfo GetCurrentGame(double length)
        {
            length = length % Length;
            var minLength = 0D;
            var maxLength = 0D;
            foreach (var user in Games)
            {
                maxLength += 1;

                if (length >= minLength && length < maxLength)
                {
                    return user;
                }

                minLength = maxLength;
            }

            return Games.First();
        }

        public GameInfo GetPreviousGame(GameInfo gameInfo)
        {
            var index = Games.IndexOf(gameInfo);
            var previousIndex = index >= 1
                ? index - 1
                : index - 1 + Games.Count;

            return Games.ElementAt(previousIndex);
        }

        public GameInfo GetPreviousGame2(GameInfo gameInfo)
        {
            var index = Games.IndexOf(gameInfo);
            var previousIndex = index >= 2
                ? index - 2
                : index - 2 + Games.Count;

            return Games.ElementAt(previousIndex);
        }

        public GameInfo GetNextGame(GameInfo gameInfo)
        {
            var index = Games.IndexOf(gameInfo);
            var nextIndex = index >= Games.Count - 1
                ? index - Games.Count + 1
                : index + 1;

            return Games.ElementAt(nextIndex);
        }

        public GameInfo GetNextGame2(GameInfo gameInfo)
        {
            var index = Games.IndexOf(gameInfo);
            var nextIndex = index >= Games.Count - 2
                ? index - Games.Count + 2
                : index + 2;

            return Games.ElementAt(nextIndex);
        }
    }
}