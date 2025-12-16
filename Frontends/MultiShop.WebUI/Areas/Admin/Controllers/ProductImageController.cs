using Humanizer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.Abstract;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;
        private readonly IFileService _fileService;

        public ProductImageController(IProductImageService productImageService, IFileService fileService)
        {
            _productImageService = productImageService;
            _fileService = fileService;
        }

        [Route("AddOrUpdateProductImage/{id}")]
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateProductImage(string id)
        {
            ProductImageDetailViewBagList();

            var value = await _productImageService.GetByIdProductImageAsync(id);

            if (value == null)
            {
                ViewBag.IsUpdate = false;
                var newDetail = new UpdateProductImageDto
                {
                    ProductId = id,
                    ProductImageId = ""
                };
                return View(newDetail);
            }

            ViewBag.IsUpdate = true;
            return View(value);
        }

        [Route("AddOrUpdateProductImage/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateProductImage(UpdateProductImageDto updateProductImageDto, IFormFile? file1, IFormFile? file2, IFormFile? file3, IFormFile? file4)
        {
            if (file1 != null)
            {
                string imagePath1 = await _fileService.UploadFileAsync(file1, "images/productDetailImages/");
                updateProductImageDto.ProductImage1 = imagePath1;
            }

            // 2. Resim Yüklendi mi?
            if (file2 != null)
            {
                string imagePath2 = await _fileService.UploadFileAsync(file2, "images/productDetailImages/");
                updateProductImageDto.ProductImage2 = imagePath2;
            }

            // 3. Resim Yüklendi mi?
            if (file3 != null)
            {
                string imagePath3 = await _fileService.UploadFileAsync(file3, "images/productDetailImages/");
                updateProductImageDto.ProductImage3 = imagePath3;
            }

            // 4. Resim Yüklendi mi?
            if (file4 != null)
            {
                string imagePath4 = await _fileService.UploadFileAsync(file4, "images/productDetailImages/");
                updateProductImageDto.ProductImage4 = imagePath4;
            }


            if (string.IsNullOrEmpty(updateProductImageDto.ProductImageId))
            {
                var createDto = new CreateProductImageDto
                {
                    ProductId = updateProductImageDto.ProductId,
                    ProductImage1 = updateProductImageDto.ProductImage1,
                    ProductImage2 = updateProductImageDto.ProductImage2,
                    ProductImage3 = updateProductImageDto.ProductImage3,
                    ProductImage4 = updateProductImageDto.ProductImage4
                };

                await _productImageService.CreateProductImageAsync(createDto);
            }
            else
            {
                await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            }

            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        [Route("ProductImageDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> ProductImageDetail(string id)
        {
            ProductImageDetailViewBagList();
            var value = await _productImageService.GetByIdProductImageAsync(id);
            return View(value);
        }

        [Route("ProductImageDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> ProductImageDetail(UpdateProductImageDto updateProductImageDto)
        {
            await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        void ProductImageDetailViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürün Resimleri";
            ViewBag.v3 = "Ürün Resim Listesi";
            ViewBag.v0 = "Ürün Resim İşlemleri";
        }
    }
}
