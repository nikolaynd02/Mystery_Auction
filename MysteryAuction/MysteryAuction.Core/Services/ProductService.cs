using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly MysteryAuctionDbContext context;

        public ProductService(MysteryAuctionDbContext _context)
        {
            this.context = _context;
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var entities = await context
                .Products
                .Include(p => p.Category)
                .ToListAsync();

            return entities
                .Select(p => new ProductViewModel()
                {
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Category = p.Category.Category,
                    ImageUrl = p.ImageUrl,
                    StartingPrice = p.StartingPrice,
                    AddedAt = p.AddedAt,
                    StartOfAuction = p.StartOfAuction,
                    EndOfAuction = p.EndOfAuction,
                    SellerId = p.SellerId
                });
        }

        public async Task AddProductAsync(AddProductViewModel model)
        {
            DateTime dateAdded = DateTime.Today;

            DateTime startDate = dateAdded.AddDays(7);
            TimeSpan ts = new TimeSpan(12, 0, 0);
            startDate = startDate.Date + ts;

            DateTime addedDateAndTime = DateTime.Now;

            DateTime endDateTime = startDate.AddDays(14);

            var entity = new Product()
            {
                ProductName = model.ProductName,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                StartingPrice = model.StartingPrice,
                AddedAt = addedDateAndTime,
                StartOfAuction = startDate,
                EndOfAuction = endDateTime,
                SellerId = model.SellerId,
                CategoryId = model.CategoryId
            };

            await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        //Might not be the right place for it.
        public async Task ChooseBuyerAsync(ProductViewModel model)
        {
            var winningBid = await context.Bids
                .Include(b => b.Product)
                .Where(b => b.Product.ProductName == model.ProductName)
                .OrderByDescending(b => b.Price)
                .ThenBy(b => b.MadeAt)
                .FirstOrDefaultAsync();

            //Test when there is no buyer
            if (winningBid == null)
            {
                return;
            }

            winningBid.HasWon = true;

            context.Bids.Update(winningBid);
            //TODO: Finish implementing. Product has to receive Buyer and User has to get product in his BoughtCollection
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            return await context.ProductsCategories.ToListAsync();
        }
    }
}
