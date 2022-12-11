using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MysteryAuction.Infrastructure.Data.Models
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DataConstraints.MysteryProductConstraints.MaxNameLength)]
        public string ProductName { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.MysteryProductConstraints.MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required] 
        public string ImageUrl { get; set; } = null!;

        public decimal StartingPrice { get; set; }

        public decimal? SoldPrice { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime StartOfAuction { get; set; }

        public DateTime EndOfAuction { get; set; }

        public int Participants { get; set; } = 0;

        public bool IsReported { get; set; }

        public bool IsOver { get; set; }

        [ForeignKey(nameof(Seller))]
        public string SellerId { get; set; } 

        public virtual MysteryAuctionUser Seller { get; set; }

        [ForeignKey(nameof(Buyer))]
        public string? BuyerId { get; set; }

        public virtual MysteryAuctionUser? Buyer { get; set; }

        [ForeignKey(nameof(Category))] 
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();

        public virtual ICollection<ProductReport> Reports { get; set; } = new HashSet<ProductReport>();

    }
}
