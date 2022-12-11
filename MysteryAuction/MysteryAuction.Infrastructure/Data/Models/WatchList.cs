using System.ComponentModel.DataAnnotations.Schema;

namespace MysteryAuction.Infrastructure.Data.Models
{
    public class WatchList
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public MysteryAuctionUser User { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
