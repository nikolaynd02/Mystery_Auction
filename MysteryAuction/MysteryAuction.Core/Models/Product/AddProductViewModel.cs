using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Models.Product
{
    public class AddProductViewModel
    {
        [Required]
        [StringLength(DataConstraints.MysteryProductConstraints.MaxNameLength, MinimumLength = DataConstraints.MysteryProductConstraints.MinNameLength)]
        public string ProductName { get; set; } = null!;

        [Required]
        [StringLength(DataConstraints.MysteryProductConstraints.MaxDescriptionLength, MinimumLength = DataConstraints.MysteryProductConstraints.MinDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Range(DataConstraints.MysteryProductConstraints.MinPrice, DataConstraints.MysteryProductConstraints.MaxPrice)]
        public decimal StartingPrice { get; set; }

        [Required]
        [ValidateNever]
        public string SellerId { get; set; } = null!;

        public Guid CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
