using System.ComponentModel.DataAnnotations;

namespace MysteryAuction.Core.Models.Bid
{
    public class AddBidViewModel
    {
        [Required] 
        public string UserId { get; set; } = null!;

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime MadeAt { get; set; }

    }
}
