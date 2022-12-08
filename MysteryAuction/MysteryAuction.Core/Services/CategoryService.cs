using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.CategoryModels;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MysteryAuctionDbContext context;

        public CategoryService(MysteryAuctionDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var entities = await context.Categories.ToListAsync();

            return entities
                .Select(e => new CategoryViewModel()
                {
                    Name = e.Name,
                });
        }

        public async Task AddAsync(AddCategoryViewModel model)
        {
            var entity = new Category()
            {
                Name = model.Name
            };

            await context.Categories.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(string name)
        {
            var entity = await context.Categories.FirstOrDefaultAsync(c => c.Name == name);

            if (entity == null)
            {
                return;
            }

            entity.IsDeleted = true;

            context.Categories.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
