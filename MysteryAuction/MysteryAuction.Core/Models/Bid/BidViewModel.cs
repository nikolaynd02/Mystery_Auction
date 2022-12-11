
namespace MysteryAuction.Core.Models.Bid
{
    public class BidViewModel
    {
        
        public string UserId { get; set; } = null!;

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public DateTime MadeAt { get; set; }

        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int Participants { get; set; }

    }
}
