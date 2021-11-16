using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Services
{
    public class UkiStreamsApi
    {
        private readonly string _url = "https://ukistreams.ru:8443/api/v1";
        private readonly HttpClient _httpClient;

        public UkiStreamsApi()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<List<RetroPlayPlatformItem>> GetRemainingByPlatform()
        {
            return await GetResult<List<RetroPlayPlatformItem>>("/retro-play/remaining");
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