using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Core.Services;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Test
{
    [TestFixture]
    public class ProductTests
    {
        private IProductService productService;
        private MysteryAuctionDbContext context;

        [SetUp]
        public async Task SetUp()
        {
            var dBBuilder = new DbContextOptionsBuilder<MysteryAuctionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new MysteryAuctionDbContext(dBBuilder.Options);

            this.productService = new ProductService(context);

            await context.Categories.AddAsync(new Category()
            {
                Name = "Car"
            });
            await context.SaveChangesAsync();

        }

        [Test]
        public async Task Test_Adding_New_Product()
        {
            var id = context.Categories.Select(c => c.Id).ToListAsync();

            var product = new AddProductViewModel()
            {
                ProductName = "Mustang",
                Description = "Mnogo e barz i moshten toz mustang ei, vroooooooooom",
                ImageUrl = "https://i.insider.com/602ee9ced3ad27001837f2ac?width=700",
                CategoryId = id
            };
        }

    }
}
