using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Models.Product;

namespace MysteryAuction.Core.Contracts
{
    public interface IWatchListService
    {
        public Task AddToCollectionAsync(Guid productId, string userId);

        public Task<IActionResult> RemoveFromCollectionAsync(Guid productId, string userId);

        public Task<IEnumerable<ProductViewModel>> GetAllAsync(string userId);
    }
}
