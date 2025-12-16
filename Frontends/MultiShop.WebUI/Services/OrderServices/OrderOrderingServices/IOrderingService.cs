using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderingServices
{
    public interface IOrderingService
    {
        Task<List<ResultOrderingByUserId>> GetOrderingByUserId(string userId);
        Task<ResultOrderByIdDto> GetOrderingById(int id);
        Task CreateOrderingAsync(CreateOrderDto createOrderingDto);
    }
}
