
using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Services;
using MysteryAuction.Infrastructure.Data.Models;
using MysteryAuction.Infrastructure.Data;

namespace MysteryAuction.Core.Test
{
    [TestFixture]
    public class WatchListTests
    {
        private IWatchListService watchListService;
        private MysteryAuctionDbContext context;

        [SetUp]
        public async Task SetUp()
        {
            var dBBuilder = new DbContextOptionsBuilder<MysteryAuctionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new MysteryAuctionDbContext(dBBuilder.Options);

            this.watchListService = new WatchListService(context);

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
        public async Task Test_Add_To_Collection()
        {
            //Arrange
            var product = await context.Products.FirstAsync();
            var user = await context.MysteryAuctionUsers.FirstAsync();

            //Act
            await watchListService.AddToCollectionAsync(product.Id,user.Id);

            //Assert
            var watchList =await  context.WatchLists.FirstAsync();

            Assert.That(watchList.UserId, Is.EqualTo(user.Id));
            Assert.That(watchList.ProductId, Is.EqualTo(product.Id));
        }

        [Test]
        public async Task Test_Remove_From_WatchList()
        {
            //Arrange
            var product = await context.Products.FirstAsync();
            var user = await context.MysteryAuctionUsers.FirstAsync();

            //Act
            await watchListService.AddToCollectionAsync(product.Id, user.Id);
            await watchListService.RemoveFromCollectionAsync(product.Id, user.Id);

            //Assert
            var watchList = await context.WatchLists.FirstOrDefaultAsync();

            Assert.IsNull(watchList);
        }

        [Test]
        public async Task? Test_Get_All_Products_For_User_From_Watchlist()
        {
            //Arrange
            var product = await context.Products.FirstAsync();
            var user = await context.MysteryAuctionUsers.FirstAsync();

            //Act
            await watchListService.AddToCollectionAsync(product.Id, user.Id);

            var collection = await watchListService.GetAllAsync(user.Id);
            //Assert

            Assert.That(collection.Count(), Is.EqualTo(1));
        }
    }
}
