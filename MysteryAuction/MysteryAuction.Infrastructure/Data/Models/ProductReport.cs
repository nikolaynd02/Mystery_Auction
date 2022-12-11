
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MysteryAuction.Infrastructure.Data.Models
{
    public class ProductReport
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey(nameof(Sender))]
        public string SenderId { get; set; } = null!;

        public MysteryAuctionUser Sender { get; set; }

        [Required]
        [MaxLength(DataConstraints.ProductReportConstraints.MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        public bool IsResolved { get; set; }
    }
}
