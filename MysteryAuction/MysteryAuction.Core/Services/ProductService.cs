using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Core.Services.MessagingService;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Infrastructure.Data.Models;
using System.Text;

namespace MysteryAuction.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly MysteryAuctionDbContext context;
        private readonly IEmailSender emailService;

        public ProductService(MysteryAuctionDbContext _context,
         IEmailSender _emailService)
        {
            this.context = _context;
            this.emailService = _emailService;
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
                //.Where(e => e.StartOfAuction.CompareTo(DateTime.Now) < 0 && !e.IsOver)
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

        public async Task ChooseBuyerAsync(ProductViewModel model)
        {
            var winningBid = await context.Bids
                .Include(b => b.Product)
                .Where(b => b.Product.ProductName == model.ProductName && b.Price > b.Product.StartingPrice)
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

            //Sends email to auction winner
            var sb = new StringBuilder();
            
            sb.AppendLine($"You have won product: {product.ProductName} with bid of {winningBid.Price:f2} BGN");
            await this.emailService.SendEmailAsync("orel_22@abv.bg", "MysteryAuction", winningBid.User.Email, $"Won auction for {product.ProductName}", sb.ToString());
            Console.WriteLine("Email send successfully");

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
