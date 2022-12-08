using Microsoft.AspNetCore.Identity;

namespace MysteryAuction.Infrastructure.Data.Models
{
    public class MysteryAuctionUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public virtual ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();

        public virtual ICollection<Product> MysteryProductsForSale { get; set; } = new HashSet<Product>();

        public virtual ICollection<Product> BoughtMysteryProducts { get; set; } = new HashSet<Product>();

    }
}
