using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using MultiShop.WebUI.Services.OrderServices.OrderingServices;
using Newtonsoft.Json;
using static Google.Apis.Requests.BatchRequest;

namespace MultiShop.WebUI.Services.OrderServices.OrderOrderingServices
{
    public class OrderingService : IOrderingService
    {
        private readonly HttpClient _httpClient;

        public OrderingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultOrderingByUserId>> GetOrderingByUserId(string userId)
        {
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7072/api/Orderings/GetOrderingByUserId/{userId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultOrderingByUserId>>(jsonData);
                return value;
            }
            return new List<ResultOrderingByUserId>();
        }

        public async Task CreateOrderingAsync(CreateOrderDto createOrderingDto)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7072/api/Orderings", createOrderingDto);
        }

        public async Task<ResultOrderByIdDto> GetOrderingById(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7072/api/Orderings/{id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<ResultOrderByIdDto>(jsonData);
            return value;
        }
    }
}
