using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MysteryAuction.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Participants = new HashSet<MysteryAuctionUser>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DataConstraints.CarConstraints.MaxMakerLength)]
        public string Maker { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.CarConstraints.MaxModelLength)]
        public string Model { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.CarConstraints.MaxEngineLength)]
        public string Engine { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        public decimal StartingPrice { get; set; }

        public decimal? SoldPrice { get; set; }


        public DateTime AddedAt { get; set; }

        public DateTime StartOfAuction { get; set; }

        public DateTime EndOfAuction { get; set; }

        public bool IsOver { get; set; }

        [ForeignKey(nameof(Seller))]
        public string SellerId { get; set; } 

        public virtual MysteryAuctionUser Seller { get; set; } 

        [ForeignKey(nameof(Buyer))]
        public string BuyerId { get; set; }

        public virtual MysteryAuctionUser Buyer { get; set; }

        public virtual ICollection<MysteryAuctionUser> Participants { get; set; }
    }
}
