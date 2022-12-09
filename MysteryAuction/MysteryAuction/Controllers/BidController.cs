using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Bid;
using System.Security.Claims;

namespace MysteryAuction.Controllers
{
    [Authorize]
    public class BidController : Controller
    {
        private readonly IBidService bidService;

        public BidController(IBidService _bidService)
        {
            this.bidService = _bidService;
        }

        [HttpGet]
        public IActionResult Add(Guid id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBidViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.UserId = userId;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await bidService.AddBidAsync(model);

                //Redirect to User bids
                return Ok();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Problem with placing a bid!");

                return View(model);
            }
        }
    }
}
