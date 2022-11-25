using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MysteryAuction.Data.Models
{
    public class MysteryAuctionUser : IdentityUser
    {
        public MysteryAuctionUser()
        {
            this.BoughtMysteryProducts = new HashSet<MysteryProduct>();
            this.MysteryProductsForSale = new HashSet<MysteryProduct>();
        }

        [Required] 
        [MaxLength(DataConstraints.UserConstraints.MaxUsernameLength)]
        public string Username { get; set; } = null!;


        public virtual ICollection<MysteryProduct> MysteryProductsForSale { get; set; }

        public virtual ICollection<MysteryProduct> BoughtMysteryProducts { get; set; }
    }
}
