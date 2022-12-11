

using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Services;
using MysteryAuction.Infrastructure.Data.Models;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Product;

namespace MysteryAuction.Core.Test
{
    [TestFixture]
    public class ProductReportTests
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

            

        }

        [Test]
        public async Task Test()
        {
            
        }
    }
}
