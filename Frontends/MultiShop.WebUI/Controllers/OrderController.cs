using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using MultiShop.WebUI.Services.Abstract;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.OrderServices.OrderAddressService;
using MultiShop.WebUI.Services.OrderServices.OrderingServices;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace MultiShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderingService _orderingService;
        private readonly IBasketService _basketService;
        private readonly IDiscountService _discountService;
        private readonly IUserService _userService;

        public OrderController(IOrderingService orderingService, IUserService userService, IBasketService basketService, IDiscountService discountService)
        {
            _orderingService = orderingService;
            _userService = userService;
            _basketService = basketService;
            _discountService = discountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Sipariş Detayı";
            ViewBag.directory3 = "Ödeme Yap";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateOrderAddressDto createOrderAddressDto)
        {
            var userValues = await _userService.GetUserInfo();

            createOrderAddressDto.UserId = userValues.Id;
            var basket = await _basketService.GetBasket();

            var basketTotal = basket.TotalPrice;
            var tax = basketTotal / 100 * 10;
            var finalTotalPrice = basketTotal + 49 + tax;

            CreateOrderDto createOrderingDto = new CreateOrderDto
            {
                UserId = userValues.Id,
                TotalPrice = finalTotalPrice,
                OrderDate = DateTime.Now,
                OrderStatus = "Sipariş Alındı",

                FirstName = createOrderAddressDto.FirstName,
                LastName = createOrderAddressDto.LastName,
                Email = createOrderAddressDto.Email,
                PhoneNumber = createOrderAddressDto.PhoneNumber,
                Country = createOrderAddressDto.Country,
                City = createOrderAddressDto.City,
                District = createOrderAddressDto.District,
                ZipCode = createOrderAddressDto.ZipCode,
                AddressLine1 = createOrderAddressDto.AddressLine1,
                AddressLine2 = createOrderAddressDto.AddressLine2,

                OrderItems = basket.BasketItems.Select(x => new CreateOrderDetailDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductImageUrl = x.ProductImageUrl,
                    ProductPrice = x.Price,
                    ProductAmount = x.Quantity,
                    ProductTotalPrice = x.Price * x.Quantity
                }).ToList()
            };

            await _orderingService.CreateOrderingAsync(createOrderingDto);
            return RedirectToAction("Index", "Payment");
        }
    }
}
