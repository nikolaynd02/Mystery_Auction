using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MysteryAuction.Infrastructure.Data.Models
{
    public class Bid
    {
        [ForeignKey(nameof(User))] 
        public string UserId { get; set; }
        public virtual MysteryAuctionUser User { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime MadeAt { get; set; }

        public bool HasWon { get; set; }
    }
}
