using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MysteryAuction.Infrastructure.Data.Models
{
    public class UnclaimedContainer
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DataConstraints.UnclaimedContainerConstraints.MaxContainerNumberLength)]
        public string ContainerNumber { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.UnclaimedContainerConstraints.MaxCheckDigitLength)]
        public string CheckDigit { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.UnclaimedContainerConstraints.MaxIsoCodeLength)]
        public string IsoCode { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.UnclaimedContainerConstraints.MaxWeightInclContainer)]
        public string MaxWeightInclContainer { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.UnclaimedContainerConstraints.MaxWeightOfContainer)]
        public string WeightOfContainer { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.UnclaimedContainerConstraints.MaxPackedWeight)]
        public string MaxPackedWeight { get; set; } = null!;

        [Required]
        [MaxLength(DataConstraints.UnclaimedContainerConstraints.MaxPackedVolume)]
        public string MaxPackedVolume { get; set; } = null!;

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

    }
}
