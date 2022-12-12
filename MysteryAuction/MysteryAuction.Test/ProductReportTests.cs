using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Services;
using MysteryAuction.Infrastructure.Data.Models;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Core.Models.ProductReport;

namespace MysteryAuction.Core.Test
{
    [TestFixture]
    public class ProductReportTests
    {
        private IProductReportService productReportService;
        private MysteryAuctionDbContext context;

        [SetUp]
        public async Task SetUp()
        {
            var dBBuilder = new DbContextOptionsBuilder<MysteryAuctionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new MysteryAuctionDbContext(dBBuilder.Options);

            this.productReportService = new ProductReportService(context);

            var category = new Category() { Name = "car" };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            await context.MysteryAuctionUsers.AddAsync(new MysteryAuctionUser()
            {
                Email = "test@mail.com"

            });
            await context.SaveChangesAsync();

            var product = new Product()
            {
                Participants = 1,
                ProductName = "Test",
                Description = "Test",
                ImageUrl = "Test",
                AddedAt = DateTime.Now,
                EndOfAuction = DateTime.Now,
                StartingPrice = 20,
                StartOfAuction = DateTime.Now,
                SellerId = "no imp",
                CategoryId = category.Id

            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

        }

        [Test]
        public async Task Test_Adding_Report_To_Product()
        {
            //Arrange
            var productId = await context.Products.FirstAsync();
            var user = await context.MysteryAuctionUsers.FirstAsync();

            var report = new AddProductReportViewModel()
            {
                Description = "Never gonna give you up, never gonna let you down",
                ProductId = productId.Id,
                SenderId = user.Id,
                IsResolved = false
            };

            //Act
            await productReportService.AddAsync(report);

            //Assert
            var reports = await context.ProductsReports.ToListAsync();

            //Equals(x, is.qeual) does not work
            Assert.AreEqual(reports.Count, 1);
        }

        [Test]
        public async Task Test_Resolve_Report()
        {
            //Arrange
            var productId = await context.Products.FirstAsync();
            var user = await context.MysteryAuctionUsers.FirstAsync();

            var report = new AddProductReportViewModel()
            {
                Description = "Never gonna give you up, never gonna let you down",
                ProductId = productId.Id,
                SenderId = user.Id,
                IsResolved = false
            };

            //Act
            await productReportService.AddAsync(report);
            var report2 = await context.ProductsReports.FirstAsync();

            await productReportService.Resolved(report2.Id);

            //Assert
            var report3 = await context.ProductsReports.FirstAsync();
            //Equals(x, is.qeual) does not work
            Assert.AreEqual(report3.IsResolved, true);
        }

        [Test]
        public async Task Test_Get_All_Reports()
        {
            //Arrange
            var productId = await context.Products.FirstAsync();
            var user = await context.MysteryAuctionUsers.FirstAsync();

            var report = new AddProductReportViewModel()
            {
                Description = "Never gonna give you up, never gonna let you down",
                ProductId = productId.Id,
                SenderId = user.Id,
                IsResolved = false
            };

            //Act
            await productReportService.AddAsync(report);

            var reports = productReportService.GetAllAsync();
            //Assert
            //Equals(x, is.qeual) does not work
            Assert.AreEqual(reports.Result.Count(), 1);
        }
    }
}
