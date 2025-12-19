using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace MultiShop.WebUI.ViewComponents.OrderViewComponents
{
    public class OrderProductSummaryViewComponent : ViewComponent
    {
        private readonly IBasketService _basketService;
        private readonly IDiscountService _discountService;

        public OrderProductSummaryViewComponent(IBasketService basketService, IDiscountService discountService)
        {
            _basketService = basketService;
            _discountService = discountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basket = await _basketService.GetBasket();

            decimal productTotal = basket.TotalPrice;
            decimal cargo = 49.00m;
            int discountRate = basket.DiscountRate ?? 0;
            decimal discountAmount = productTotal / 100 * discountRate;
            decimal finalTotalPrice = (productTotal - discountAmount) + cargo;

            ViewBag.ProductTotal = productTotal;
            ViewBag.Cargo = cargo;
            ViewBag.DiscountRate = discountRate;
            ViewBag.DiscountAmount = discountAmount;
            ViewBag.FinalTotalPrice = finalTotalPrice;

            var basketTotal = await _basketService.GetBasket();
            var basketItems = basketTotal.BasketItems;
            return View(basketItems);
        }
    }
}
