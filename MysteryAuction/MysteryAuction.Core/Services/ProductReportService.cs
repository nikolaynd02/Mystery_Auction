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
                .OrderBy(r => r.IsResolved)
                .ToListAsync();

            return entities.Select(r => new ProductReportViewModel()
            {
                Id = r.Id,
                SenderEmail = r.Sender.Email,
                ProductName = r.Product.ProductName,
                Description = r.Description,
                IsResolved = r.IsResolved
            });
        }

        public async Task Resolved(Guid id)
        {
            var entity = await context.ProductsReports.FindAsync(id);

            if (entity == null)
            {
                return;
            }

            entity.IsResolved = true;

            context.ProductsReports.Update(entity);
            await context.SaveChangesAsync();

        }
    }
}
