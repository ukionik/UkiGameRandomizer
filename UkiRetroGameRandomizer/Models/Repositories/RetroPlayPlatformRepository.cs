using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UkiRetroGameRandomizer.Models.Data;
using UkiRetroGameRandomizer.Models.Services;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public class RetroPlayPlatformRepository : IRetroPlayPlatformRepository
    {
        public List<RetroPlayPlatformItem> Data { get; private set; }

        public async Task LoadAsync()
        {
            try
            {
                Data = await new UkiStreamsApi().GetRemainingByPlatform();
            }
            catch (Exception)
            {
                Data = new List<RetroPlayPlatformItem>();
            }
        }
    }
}