using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Services
{
    public class UkiStreamsApi
    {
        private readonly string _url = "https://api.ukistreams.ru/api/v1";
        private readonly HttpClient _httpClient;

        public UkiStreamsApi()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<List<RetroPlayPlatformItem>> GetRemainingByPlatform()
        {
            return await GetResult<List<RetroPlayPlatformItem>>("/retro-play/remaining");
        }
        
        public async Task<List<WheelItem>> GetWheelItems()
        {
            return await GetResult<List<WheelItem>>("/rgg/randomizer-wheel-items/2");
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