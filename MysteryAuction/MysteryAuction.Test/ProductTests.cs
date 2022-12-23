using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.CategoryModels;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Core.Services;
using MysteryAuction.Core.Services.MessagingService;
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
            //Email won't be tested
            IEmailSender emailSender = null;
            this.productService = new ProductService(context, emailSender);

            await context.Categories.AddAsync(new Category()
            {
                Name = "Car"
            });
            await context.SaveChangesAsync();

            await context.MysteryAuctionUsers.AddAsync(new MysteryAuctionUser()
            {
                Email = "test@mail.com"
                
            });
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task Test_Adding_New_Product()
        {
            //Arrange
            var id = context.Categories.First();

            var product = new AddProductViewModel()
            {
                ProductName = "Mustang",
                Description = "Mnogo e barz i moshten toz mustang ei, vroooooooooom",
                ImageUrl = "https://i.insider.com/602ee9ced3ad27001837f2ac?width=700",
                CategoryId = id.Id,
                SellerId = "test",
                StartingPrice = 20

            };
            //Act
            await productService.AddProductAsync(product);

            //Assert
            Assert.That( context.Products.Count(), Is.EqualTo(1));


        }


        [Test]
        public async Task Test_Get_Product_By_id()
        {
            //Arrange
            var category = context.Categories.First();

            var user = context.MysteryAuctionUsers.First();

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
                SellerId = user.Id,
                CategoryId = category.Id
                
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            //Act
            var result = productService.GetProductAsync(product.Id);
            //Assert
            Assert.NotNull(result);
            Assert.That(result.Result.Id, Is.EqualTo(product.Id));

        }

        [Test]
        public async Task Test_Get_All_Products()
        {
            //Arrange
            var category = context.Categories.First();
            var user = context.MysteryAuctionUsers.First();

            var product = new Product()
            {

                Participants = 11,
                ProductName = "Test",
                Description = "Test",
                ImageUrl = "Test",
                AddedAt = DateTime.Now,
                EndOfAuction = DateTime.Now,
                SoldPrice = 20,
                StartingPrice = 20,
                StartOfAuction = DateTime.Now,
                SellerId = user.Id,
                CategoryId = category.Id
            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            //Act
            var collection = await productService.GetAllProductsAsync();
            //Assert
            Assert.NotNull(collection);
            Assert.That(collection.Count(), Is.EqualTo(1));

        }

        [Test]
        public async Task Test_Get_All_Categories()
        {


            await productService.GetAllCategoriesAsync();


            Assert.That(productService.GetAllCategoriesAsync().Result.Count(), Is.EqualTo(1));

        }
    }
}