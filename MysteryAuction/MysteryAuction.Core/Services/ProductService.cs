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

        public async Task<ProductViewModel> GetProductAsync(Guid id)
        {
            var entity = await context.Products
                .Include(p=>p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (entity == null)
            {
                return null;
            }

            var product = new ProductViewModel()
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Description = entity.Description,
                Category = entity.Category.Name,
                ImageUrl = entity.ImageUrl,
                StartingPrice = entity.StartingPrice,
                Participants = entity.Participants,
                AddedAt = entity.AddedAt,
                StartOfAuction = entity.StartOfAuction,
                EndOfAuction = entity.EndOfAuction,
                SellerId = entity.SellerId
            };

            return product;

        }

        //TODO: uncomment where in query, it is commented for test
        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var entities = await context
                .Products
                .Include(p => p.Category)
                .Where(e => e.StartOfAuction.CompareTo(DateTime.Now) < 0 && !e.IsOver)
                .ToListAsync();

            return entities
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    StartingPrice = p.StartingPrice,
                    Participants = p.Participants,
                    AddedAt = p.AddedAt,
                    StartOfAuction = p.StartOfAuction,
                    EndOfAuction = p.EndOfAuction,
                    SellerId = p.SellerId
                });
        }

        public async Task<IEnumerable<ProductViewModel>> GetUserBoughtProductsAsync(string userId)
        {
            var entities = await context.Products
                .Include(p => p.Category)
                .Where(p => p.BuyerId == userId)
                .ToListAsync();

            return entities
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    StartingPrice = p.StartingPrice,
                    Participants = p.Participants,
                    SoldPrice = p.SoldPrice,
                    AddedAt = p.AddedAt,
                    StartOfAuction = p.StartOfAuction,
                    EndOfAuction = p.EndOfAuction,
                    SellerId = p.SellerId
                });
        }

        public async  Task<IEnumerable<ProductViewModel>> GetUserProductsForSaleAsync(string userId)
        {
            var entities = await context.Products
                .Include(p => p.Category)
                .Where(p => p.SellerId == userId)
                .ToListAsync();

            return entities
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    StartingPrice = p.StartingPrice,
                    Participants = p.Participants,
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

            var product = await context.Products.FindAsync(model.Id);

            product!.IsOver = true;

            context.Products.Update(product);
            await context.SaveChangesAsync();

            //Test when there is no buyer
            if (winningBid == null)
            {
                return;
            }

            

            winningBid.Product.IsOver = true;



            winningBid.HasWon = true;

            context.Bids.Update(winningBid);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
