using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UkiRetroGameRandomizer.Models.Data;
using UkiRetroGameRandomizer.Models.Services;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public class DroppedGameRepository : IDroppedGameRepository
    {
        public List<DroppedGame> Data { get; private set; }

        public async Task LoadAsync()
        {
            try
            {
                Data = await new DropmaniaApi().GetDroppedGames();
            }
            catch (Exception)
            {
                Data = new List<DroppedGame>();
            }
        }
    }
}