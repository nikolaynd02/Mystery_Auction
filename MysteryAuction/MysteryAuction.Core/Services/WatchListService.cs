
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Services
{
    public class WatchListService : IWatchListService
    {
        private readonly MysteryAuctionDbContext context;

        public WatchListService(MysteryAuctionDbContext _context)
        {
            this.context = _context;
        }

        public async Task AddToCollectionAsync(Guid id, string userId)
        {
            var watchList = new WatchList()
            {
                ProductId = id,
                UserId = userId
            };

            await context.WatchLists.AddAsync(watchList);
            await context.SaveChangesAsync();
        }

        public async Task<IActionResult> RemoveFromCollectionAsync(Guid productId, string userId)
        {
            var entity = await context.WatchLists
                .FirstOrDefaultAsync(e => 
                    e.ProductId == productId
                    && e.UserId == userId);

            //Needs to be changed
            if (entity == null)
            {
                return null;
            }

            context.WatchLists.Remove(entity);
            await context.SaveChangesAsync();

            return null;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(string userId)
        {
            var entities = await context.WatchLists
                .Include(p => p.Product)
                .ThenInclude(p => p.Category)
                .Where(w => w.UserId == userId)
                .ToListAsync();

            return entities
                .Select(w => new ProductViewModel()
                {
                    Id = w.Product.Id,
                    ProductName = w.Product.ProductName,
                    Description = w.Product.Description,
                    Category = w.Product.Category.Name,
                    ImageUrl = w.Product.ImageUrl,
                    StartingPrice = w.Product.StartingPrice,
                    Participants = w.Product.Participants,
                    SoldPrice = w.Product.SoldPrice,
                    AddedAt = w.Product.AddedAt,
                    StartOfAuction = w.Product.StartOfAuction,
                    EndOfAuction = w.Product.EndOfAuction,
                    SellerId = w.Product.SellerId
                });
        }
    }
}
