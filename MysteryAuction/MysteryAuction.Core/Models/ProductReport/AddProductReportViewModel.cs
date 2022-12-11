
using MysteryAuction.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MysteryAuction.Core.Models.ProductReport
{
    public class AddProductReportViewModel
    {
        [ValidateNever]
        public Guid ProductId { get; set; }

        [ValidateNever]
        public string SenderId { get; set; } = null!;

        [Required]
        [StringLength(DataConstraints.ProductReportConstraints.MaxDescriptionLength, MinimumLength = DataConstraints.ProductReportConstraints.MinDescriptionLength)]
        public string Description { get; set; } = null!;

        public bool IsResolved { get; set; } 
    }
}
