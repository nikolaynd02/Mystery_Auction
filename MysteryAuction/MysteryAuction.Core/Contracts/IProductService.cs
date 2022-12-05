using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysteryAuction.Core.Models.Product;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Contracts
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();

        public Task AddProductAsync(AddProductViewModel model);

        public Task ChooseBuyerAsync(ProductViewModel model);

        public Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();
    }
}
