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

        public async Task<IViewComponentResult> InvokeAsync(string code)
        {
            var values = await _discountService.GetDiscountCouponCountRate(code);
            var getBasket = await _basketService.GetBasket();
            ViewBag.total = getBasket.TotalPrice;
            var totalPriceWithTax = getBasket.TotalPrice + 49 +  getBasket.TotalPrice / 100 * 10;
            var tax = getBasket.TotalPrice / 100 * 10;
            ViewBag.tax = tax;

            var totalNewPriceWithDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * values);
            ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;

            var basketTotal = await _basketService.GetBasket();
            var basketItems = basketTotal.BasketItems;
            return View(basketItems);
        }
    }
}
