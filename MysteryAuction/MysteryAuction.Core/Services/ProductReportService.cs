using Microsoft.EntityFrameworkCore;
using MysteryAuction.Core.Contracts;
using MysteryAuction.Core.Models.ProductReport;
using MysteryAuction.Infrastructure.Data;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Services
{
    public class ProductReportService : IProductReportService
    {
        private readonly MysteryAuctionDbContext context;

        public ProductReportService(MysteryAuctionDbContext _context)
        {
            this.context = _context;
        }


        public async Task AddAsync(AddProductReportViewModel model)
        {
            var report = new ProductReport()
            {
                Description = model.Description,
                SenderId = model.SenderId,
                ProductId = model.ProductId
            };

            await context.ProductsReports.AddAsync(report);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductReportViewModel>> GetAllAsync()
        {
            var entities = await context.ProductsReports
                .Include(r => r.Product)
                .Include(r => r.Sender)
                .ToListAsync();

            return entities.Select(r => new ProductReportViewModel()
            {
                SenderEmail = r.Sender.Email,
                ProductName = r.Product.ProductName,
                Description = r.Product.Description,
                IsResolved = r.IsResolved
            });
        }
    }
}
