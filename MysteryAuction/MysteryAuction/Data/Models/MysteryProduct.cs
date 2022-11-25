using System.ComponentModel.DataAnnotations;

namespace MysteryAuction.Data.Models
{
    public class MysteryProduct
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DataConstraints.MysteryProductConstraints.MaxNameLength)]
        public string ProductName { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.MysteryProductConstraints.MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        public decimal StartingPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime StartOfAuction { get; set; }

        public DateTime EndOfAuction { get; set; }

        [Required] 
        public string SellerId { get; set; } = null!;

        public string BuyerId { get; set; } = null!;

    }
}
