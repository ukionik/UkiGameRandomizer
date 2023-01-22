using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Services
{
    public class DropmaniaApi
    {
        private readonly string _url = "https://api.ukistreams.ru/api/v1";
        private readonly HttpClient _httpClient;

        public DropmaniaApi()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<DroppedGame>> GetDroppedGames()
        {
            return await GetResult<List<DroppedGame>>("/dropmania-game/dropped");
        }
        
        public async Task<List<WheelItem>> GetWheelItems()
        {
            return await GetResult<List<WheelItem>>("/dropmania/wheel-items");
        }
        
        public async Task<List<DropmaniaRolledGame>> GetRolledGames()
        {
            return await GetResult<List<DropmaniaRolledGame>>("/dropmania/rolled-games");
        }
        
        private async Task<T> GetResult<T>(string url)
        {
            var fullUrl = $"{_url}{url}";
            var responseMessage = await _httpClient.GetAsync(fullUrl);
            var result = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}