using MysteryAuction.Core.Models.CategoryModels;

namespace MysteryAuction.Core.Contracts
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();

        public Task AddAsync(AddCategoryViewModel model);

        public Task RemoveAsync(string name);
    }
}
