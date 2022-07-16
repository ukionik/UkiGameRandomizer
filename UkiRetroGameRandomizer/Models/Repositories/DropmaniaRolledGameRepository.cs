using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UkiRetroGameRandomizer.Models.Data;
using UkiRetroGameRandomizer.Models.Services;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public class DropmaniaRolledGameRepository : IDropmaniaRolledGameRepository
    {
        public List<DropmaniaRolledGame> Data { get; private set; }
        public async Task LoadAsync()
        {
            try
            {
                Data = await new DropmaniaApi().GetRolledGames();
            }
            catch (Exception)
            {
                Data = new List<DropmaniaRolledGame>();
            }
        }
    }
}