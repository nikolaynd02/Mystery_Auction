
using MysteryAuction.Core.Models.ProductReport;

namespace MysteryAuction.Core.Contracts
{
    public interface IProductReportService
    {
        public Task AddAsync(AddProductReportViewModel model);

        public Task<IEnumerable<ProductReportViewModel>> GetAllAsync();

    }
}
