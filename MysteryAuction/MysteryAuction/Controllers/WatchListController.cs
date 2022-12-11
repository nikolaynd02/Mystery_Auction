using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Core.Services;
using System.Security.Claims;

namespace MysteryAuction.Controllers
{
    public class WatchListController : Controller
    {
        private readonly IWatchListService watchListService;
        public WatchListController(IWatchListService _watchListService)
        {
            this.watchListService = _watchListService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await watchListService.AddToCollectionAsync(Id, userId);

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model =  await watchListService.GetAllAsync(userId);

           return View(model);
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await watchListService.RemoveFromCollectionAsync(id, userId);

            return RedirectToAction("All");
        }
    }
}
