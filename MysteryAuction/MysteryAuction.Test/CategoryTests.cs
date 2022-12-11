using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.CategoryModels;
using MysteryAuction.Core.Services;
using MysteryAuction.Infrastructure.Data;

namespace MysteryAuction.Core.Test
{
    [TestFixture]
    public class Tests
    {
        private ICategoryService categoryService;
        private MysteryAuctionDbContext context;

        [SetUp]
        public void Setup()
        {
            var dBBuilder = new DbContextOptionsBuilder<MysteryAuctionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new MysteryAuctionDbContext(dBBuilder.Options);

            this.categoryService = new CategoryService(context);
        }

        [Test]
        public async Task Test_Adding_New_Category()
        {
            //Arrange
            var entity = new AddCategoryViewModel()
            {
                Name = "Ships"
            };
            //Act
            await categoryService.AddAsync(entity);
            await categoryService.AddAsync(entity);

            //Assert
            Assert.That(context.Categories.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task Test_Deleting_Category()
        {
            //Arrange
            var entity = new AddCategoryViewModel()
            {
                Name = "Ships"
            };

            //Act
            await categoryService.AddAsync(entity);
            await categoryService.RemoveAsync("Ships");

            //Assert
            Assert.That(context.Categories.FirstOrDefaultAsync().Result!.IsDeleted, Is.EqualTo(true));
        }

        [Test]
        public async Task Test_Getting_All_Categories()
        {
            //Arrange
            var entity = new AddCategoryViewModel()
            {
                Name = "Ships"
            };

            var entity2 = new AddCategoryViewModel()
            {
                Name = "Vases"
            };
            //Act
            await categoryService.AddAsync(entity);
            await categoryService.AddAsync(entity2);

            //Assert
            Assert.That(categoryService.GetAllCategoriesAsync().Result.Count(), Is.EqualTo(2));
        }
    }
}