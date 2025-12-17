using MultiShop.Basket.WebAPI.Dtos;

namespace MultiShop.Basket.WebAPI.Services
{
    public interface IBasketService
    {
        Task<BasketTotalDto> GetBasket(string userId);
        Task SaveBasket(BasketTotalDto basketTotalDto);
        Task AddItemToBasketAsync(BasketItemDto basketItemDto, string userId);
        Task DeleteBasket(string userId);
    }
}
