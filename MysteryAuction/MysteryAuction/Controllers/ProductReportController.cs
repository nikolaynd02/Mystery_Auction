using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.ProductReport;
using MysteryAuction.Core.Services;
using System.Security.Claims;

namespace MysteryAuction.Controllers
{
    public class ProductReportController : Controller
    {
        private readonly IProductReportService productReportService;

        public ProductReportController(IProductReportService _productReportService)
        {
            this.productReportService = _productReportService;
        }

        [HttpGet]
        public IActionResult Add(Guid id)
        {
            var model = new AddProductReportViewModel()
            {
                ProductId = id,
                SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductReportViewModel model)
        {
            var sanitizer = new HtmlSanitizer();
            model.Description = sanitizer.Sanitize(model.Description);

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.SenderId = userId;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await productReportService.AddAsync(model);

                return RedirectToAction("All","Product");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Problem with reporting product!");

                return View(model);
            }
        }
    }
}
