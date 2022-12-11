
namespace MysteryAuction.Core.Models.Product
{
    public class ProductViewModel
    {
        public Guid Id { get; set; } 

        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int Participants { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal? SoldPrice { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime StartOfAuction { get; set; }

        public DateTime EndOfAuction { get; set; }

        public string SellerId { get; set; } = null!;

        public string? Buyer { get; set; }
    }
}
