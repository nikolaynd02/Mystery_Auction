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

            this.CarsForSale = new HashSet<Car>();
            this.BoughtCars = new HashSet<Car>();

            this.UnclaimedContainersForSale = new HashSet<UnclaimedContainer>();
            this.BoughtUnclaimedContainers = new HashSet<UnclaimedContainer>();
        }



        public virtual ICollection<MysteryProduct> MysteryProductsForSale { get; set; }

        public virtual ICollection<MysteryProduct> BoughtMysteryProducts { get; set; }

        public virtual ICollection<Car> CarsForSale { get; set; }

        public virtual ICollection<Car> BoughtCars{ get; set; }

        public virtual ICollection<UnclaimedContainer> UnclaimedContainersForSale { get; set; }

        public virtual ICollection<UnclaimedContainer> BoughtUnclaimedContainers { get; set; }
    }
}
