using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.CategoryModels;

namespace MysteryAuction.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            this.categoryService = _categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await categoryService.GetAllCategoriesAsync();

            return View(model);
        }

        [HttpGet]
        public  IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await categoryService.AddAsync(model);

                return RedirectToAction("All", "Category");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Problem with adding category!");

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string categoryName)
        {
            try
            {
                await categoryService.RemoveAsync(categoryName);

                return RedirectToAction(nameof(All));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Problem with removing category");

                return RedirectToAction(nameof(All));
            }
        }
    }
}
