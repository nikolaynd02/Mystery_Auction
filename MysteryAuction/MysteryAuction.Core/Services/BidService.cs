using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Bid;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Services
{
    public class BidService : IBidService
    {
        private readonly MysteryAuctionDbContext context;

        public BidService(MysteryAuctionDbContext _context)
        {
            this.context = _context;
        }

        public async Task AddBidAsync(AddBidViewModel model)
        {
            var entity = new Bid()
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Price = model.Price,
                MadeAt = DateTime.Now,
                HasWon = false
            };

            await context.Bids.AddAsync(entity);
            await context.SaveChangesAsync();
        }

    }
}
