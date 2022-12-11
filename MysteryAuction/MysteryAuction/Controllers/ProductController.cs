using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Core.Services;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
        }

        [HttpGet]
        public async Task<IActionResult> Check(Guid id)
        {
            var model = await productService.GetProductAsync(id);

            if (model == null)
            {
                return RedirectToAction("All", "Product");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await productService.GetAllProductsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddProductViewModel()
            {
                Categories = await productService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            model.SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (!ModelState.IsValid)
            {
                model.Categories = await productService.GetAllCategoriesAsync();
                return View(model);
            }

            try
            {
                await productService.AddProductAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("","Problem with adding product!");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Bought()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await productService.GetUserBoughtProductsAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Listed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await productService.GetUserProductsForSaleAsync(userId);

            return View(model);
        }

    }
}
