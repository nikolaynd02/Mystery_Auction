using MysteryAuction.Core.Models.Product;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Contracts
{
    public interface IProductService
    {
        public Task<ProductViewModel> GetProductAsync(Guid id);
        public Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();

        public Task<IEnumerable<ProductViewModel>> GetUserBoughtProductsAsync(string userId);

        public Task<IEnumerable<ProductViewModel>> GetUserProductsForSaleAsync(string userId);

        public Task AddProductAsync(AddProductViewModel model);

        public Task ChooseBuyerAsync(ProductViewModel model);

        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
