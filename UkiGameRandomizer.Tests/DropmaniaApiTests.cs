using System;
using System.Threading.Tasks;
using NUnit.Framework;
using UkiRetroGameRandomizer.Models.Services;

namespace UkiGameRandomizer.Tests
{
    public class DropmaniaApiTests
    {
        private DropmaniaApi _dropmaniaApi;

        [SetUp]
        public void Setup()
        {
            _dropmaniaApi = new DropmaniaApi();
        }

        [Test]
        public async Task GetDroppedGames()
        {
            var droppedGames = await _dropmaniaApi.GetDroppedGames();
            foreach (var droppedGame in droppedGames)
            {
                    Console.WriteLine(droppedGame);
            }
            
            await Task.Delay(3000);
        }
        
        [Test]
        public async Task GetWheelItems()
        {
            var wheelItems = await _dropmaniaApi.GetWheelItems();
            foreach (var wheelItem in wheelItems)
            {
                Console.WriteLine(wheelItem);
                Console.WriteLine(wheelItem.Description);
            }
            
            await Task.Delay(3000);
        }
    }
}