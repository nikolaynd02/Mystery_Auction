using Microsoft.EntityFrameworkCore;
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
            var entity =
                await context.Bids
                    .FirstOrDefaultAsync(b => b.UserId == model.UserId && b.ProductId == model.ProductId);

            if (entity != null)
            {
                return;
            }

            var bids = await GetUserBids(model.UserId);

            if (bids.Any(b => b.UserId == model.UserId))
            {
                return;
            }
            var newBid = new Bid()
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Price = model.Price,
                MadeAt = DateTime.Now,
                HasWon = false
            };



            var product = await context.Products.FindAsync(model.ProductId);


            product!.Participants += 1;

            context.Products.Update(product);

            await context.Bids.AddAsync(newBid);
            await context.SaveChangesAsync();
        }

        //When Testing
        //Comment Then include Category and in the binding comment category 
        public async Task<IEnumerable<BidViewModel>> GetUserBids(string userId)
        {
            var entities = await context.Bids
                .Include(b => b.Product)
                .ThenInclude(p => p.Category)
                .Include(b => b.User)
                .Where(b => b.UserId == userId && b.Product.IsOver == false)
                .ToListAsync();

            return entities
                .Select(b => new BidViewModel()
                {
                    UserId = b.UserId,
                    ProductId = b.ProductId,
                    Price = b.Price,
                    Category = b.Product.Category.Name,
                    Description = b.Product.Description,
                    ImageUrl = b.Product.ImageUrl,
                    MadeAt = b.MadeAt,
                    Participants = b.Product.Participants,
                    ProductName = b.Product.ProductName
                });
        }
    }
}
