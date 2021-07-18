using System;
using System.Collections.Generic;

namespace GameRandomizerEngine
{
    public class GameRandomizer
    {
        private readonly int _dueTime;
        private readonly double _startSpeed;
        private double _changeIn = 1;

        public GameInfoCollection Games { get; }

        public double Length { get; private set; }
        public double Point => Length % Games.Length;

        public GameRandomizer(IEnumerable<GameInfo> games, int dueTime, int seed)
        {
            _dueTime = dueTime;
            Games = new GameInfoCollection(games);
            var random = new Random(seed);
            Games.Shuffle(random);
            _startSpeed = random.NextDouble(0.8, 1.2);
        }

        public GameInfo GetGame(int time)
        {
            var value = EaseOutQuad(time, 0, _changeIn, _dueTime);
            var speed = _startSpeed - value * _startSpeed;
            Length += speed;
            return Games.GetCurrentGame(Length);
        }

        private double EaseOutQuad(int time, double startValue, double changeIn, int duration)
        {
            var timeD = (double)time / duration;
            return -changeIn * timeD * (timeD - 2) + startValue;
        }
    }
}