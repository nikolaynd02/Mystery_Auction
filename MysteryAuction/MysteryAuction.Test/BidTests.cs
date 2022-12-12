

using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.Bid;
using MysteryAuction.Core.Services;
using MysteryAuction.Infrastructure.Data.Models;
using MysteryAuction.Infrastructure.Data;

namespace MysteryAuction.Core.Test
{
    [TestFixture]
    public class BidTests
    {
        private IBidService bidService;
        private MysteryAuctionDbContext context;

        [SetUp]
        public async Task SetUp()
        {
            var dBBuilder = new DbContextOptionsBuilder<MysteryAuctionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new MysteryAuctionDbContext(dBBuilder.Options);

            this.bidService = new BidService(context);

            await context.MysteryAuctionUsers.AddAsync(new MysteryAuctionUser()
            {
                Email = "test@mail.com"

            });
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task Test_Adding_Bid()
        {
            //Arrange
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
                BuyerId = "test",
                SellerId = "test",


            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var bid = new AddBidViewModel
            {
                UserId = "test",
                Price = 20,
                ProductId = product.Id,
                MadeAt = DateTime.Now

            };
            //Act
            await bidService.AddBidAsync(bid);
            //Assert
            var bid2 = await context.Bids.FirstAsync();

            //does not work with Equal(x, Is.Equal)
            Assert.AreEqual(bid2.Price, 20);
        }


        //Go to BidService and comment part of the db query
        [Test]
        public async Task Test_Get_User_Bids()
        {
            //Arrange
            var product = new Product()
            {

                Participants = 1,
                ProductName = "Test",
                Description = "Test",
                ImageUrl = "Test",
                AddedAt = DateTime.Now,
                EndOfAuction = DateTime.Now,
                SoldPrice = 20,
                StartingPrice = 20,
                StartOfAuction = DateTime.Now,
                SellerId = "test",

            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var user = context.MysteryAuctionUsers.First();

            var bid = new Bid()
            {
                UserId = user.Id,
                Price = 20,
                ProductId = product.Id,
                MadeAt = DateTime.Now
            };

            await context.Bids.AddAsync(bid);
            await context.SaveChangesAsync();
            //Act
            var collection = await bidService.GetUserBids(user.Id);
            var collection2 = await bidService.GetUserBids("NoUser");

            //Assert
            Assert.That(collection.Count(), Is.EqualTo(1));
            Assert.That(collection2.Count(), Is.EqualTo(0));
        }
    }
}
