
namespace MysteryAuction.Core.Models.ProductReport
{
    public class ProductReportViewModel
    {
        public Guid Id { get; set; }
        public string SenderEmail { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsResolved { get; set; }
    }
}
