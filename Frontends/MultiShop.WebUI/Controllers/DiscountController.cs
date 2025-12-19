using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;

        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                TempData["DiscountError"] = "Lütfen bir kupon kodu giriniz.";
                return RedirectToAction("Index", "ShoppingCart");
            }

            var discountRate = await _discountService.GetDiscountCouponCountRate(code);

            if (discountRate == 0)
            {
                TempData["DiscountError"] = "Geçersiz veya süresi dolmuş bir kupon kodu.";
                return RedirectToAction("Index", "ShoppingCart");
            }

            var basketValues = await _basketService.GetBasket();
            basketValues.DiscountRate = discountRate; 
            basketValues.DiscountCode = code;
            await _basketService.SaveBasket(basketValues);

            var cargo = 49;
            var totalNewPriceWithDiscount = (basketValues.TotalPrice - (basketValues.TotalPrice / 100 * discountRate)) + cargo;

            TempData["DiscountSuccess"] = "Kupon başarıyla uygulandı!";
            return RedirectToAction("Index", "ShoppingCart", new { code = code, discountRate = discountRate, totalNewPriceWithDiscount = totalNewPriceWithDiscount });
        }
    }
}
