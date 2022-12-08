using System.ComponentModel.DataAnnotations;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Core.Models.CategoryModels
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(DataConstraints.CategoryConstraints.MaxNameLength,
            MinimumLength = DataConstraints.CategoryConstraints.MinNameLength)]
        public string Name { get; set; } = null!;
    }
}
