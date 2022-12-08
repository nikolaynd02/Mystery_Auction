using MysteryAuction.Core.Models.Product;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Contracts
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();

        public Task AddProductAsync(AddProductViewModel model);

        public Task ChooseBuyerAsync(ProductViewModel model);

        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
