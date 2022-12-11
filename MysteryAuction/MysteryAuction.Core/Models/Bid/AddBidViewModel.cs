using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Models.Bid
{
    public class AddBidViewModel
    {
        [Required]
        [ValidateNever]
        public string UserId { get; set; } = null!;

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [Range(DataConstraints.MysteryProductConstraints.MinPrice, DataConstraints.MysteryProductConstraints.MaxPrice)]
        public decimal Price { get; set; }

        public DateTime MadeAt { get; set; } 

    }
}
