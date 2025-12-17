using MultiShop.Basket.WebAPI.Dtos;
using MultiShop.Basket.WebAPI.Settings;
using System.Text.Json;

namespace MultiShop.Basket.WebAPI.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<BasketTotalDto> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
            {
                return null;
            }
            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket);
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _redisService.GetDb().StringSetAsync(basketTotalDto.UserId, JsonSerializer.Serialize(basketTotalDto));
        }

        public async Task DeleteBasket(string userId)
        {
            await _redisService.GetDb().KeyDeleteAsync(userId);
        }

        public async Task AddItemToBasketAsync(BasketItemDto basketItemDto, string userId)
        {
            var existBasket = await GetBasket(userId);

            if (existBasket == null)
            {
                existBasket = new BasketTotalDto
                {
                    UserId = userId,
                    BasketItems = new List<BasketItemDto>()
                };
            }

            var existingItem = existBasket.BasketItems.FirstOrDefault(x =>
                x.ProductId == basketItemDto.ProductId &&
                AreAttributesEqual(x.SelectedAttributes, basketItemDto.SelectedAttributes));

            if (existingItem != null)
            {
                existingItem.Quantity += basketItemDto.Quantity;
            }
            else
            {
                existBasket.BasketItems.Add(basketItemDto);
            }

            await SaveBasket(existBasket);
        }

        private bool AreAttributesEqual(Dictionary<string, string> dict1, Dictionary<string, string> dict2)
        {
            if ((dict1 == null || dict1.Count == 0) && (dict2 == null || dict2.Count == 0)) return true;

            if (dict1 == null || dict2 == null) return false;

            if (dict1.Count != dict2.Count) return false;

            foreach (var kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out string value2)) return false;
                if (kvp.Value != value2) return false;
            }
            return true;
        }
    }
}
