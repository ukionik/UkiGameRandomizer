using System;
using System.Collections.Generic;

namespace GameRandomizerEngine
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list, Random r)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = r.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static double NextDouble(this Random r, double minimum, double maximum)
        {
            return r.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}