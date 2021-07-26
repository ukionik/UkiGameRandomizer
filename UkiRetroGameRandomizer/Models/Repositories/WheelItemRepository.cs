using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UkiRetroGameRandomizer.Models.Data;
using UkiRetroGameRandomizer.Models.Services;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public class WheelItemRepository : IWheelItemRepository
    {
        public List<WheelItem> Data { get; private set; }

        public async Task LoadAsync()
        {
            try
            {
                Data = await new DropmaniaApi().GetWheelItems();
            }
            catch (Exception)
            {
                Data = new List<WheelItem>();
            }
            
        }

        public WheelItem FindByName(string name)
        {
            return Data.Single(x => x.Title == name);
        }
    }
}