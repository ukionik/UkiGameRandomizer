
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GameRandomizerEngine.Tests
{
    [TestFixture]
    public class CleanRandomTest
    {
        [Test]
        public void Test()
        {
            var r = new Random(DateTime.Now.Millisecond);

            var count = 100;
            var dict = new Dictionary<int, int>();

            for (var i = 0; i < count; i++)
            {
                var value = r.Next(1753);
                if (dict.ContainsKey(value))
                {
                    dict[value]++;
                }
                else
                {
                    dict[value] = 1;
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
        public void TestBetterRandom()
        {
            var provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[4];

            var count = 100;
            var dict = new Dictionary<int, int>();

            for (var i = 0; i < count; i++)
            {
                provider.GetBytes(byteArray);
                var randomInteger = BitConverter.ToUInt32(byteArray, 0);
                var value = (int)(randomInteger % 1753);
                if (dict.ContainsKey(value))
                {
                    dict[value]++;
                }
                else
                {
                    dict[value] = 1;
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
        public void Test2()
        {
            var random = new Random();
            Console.WriteLine(random.Next(1,3));
            Console.WriteLine(random.Next(1,3));
            Console.WriteLine(random.Next(1,3));
            Console.WriteLine(random.Next(1,3));
        }

    }
}