using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Contracts;

namespace MysteryAuction.Areas.Admin.Controllers
{
    public class ProductReportController : BaseController
    {
        private readonly IProductReportService productReportService;

        public ProductReportController(IProductReportService _productReportService)
        {
            this.productReportService = _productReportService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await productReportService.GetAllAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Resolved(Guid id)
        {
            await productReportService.Resolved(id);

            return RedirectToAction("All");
        }
    }
}
