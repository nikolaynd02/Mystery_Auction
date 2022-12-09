
namespace MysteryAuction.Core.Models.Product
{
    public class ProductViewModel
    {
        public string Id { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal StartingPrice { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime StartOfAuction { get; set; }

        public DateTime EndOfAuction { get; set; }

        public string SellerId { get; set; } = null!;

        public string? Buyer { get; set; }
    }
}
