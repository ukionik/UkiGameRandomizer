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
        private readonly string _url = "https://dropmania.sneschallenge.com/api";
        private readonly HttpClient _httpClient;

        public DropmaniaApi()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<DroppedGame>> GetDroppedGames()
        {
            return await GetResult<List<DroppedGame>>("/dropmania/other/dropedGames");
        }
        
        public async Task<List<WheelItem>> GetWheelItems()
        {
            return await GetResult<List<WheelItem>>("/dropmania/other/wheelItems");
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