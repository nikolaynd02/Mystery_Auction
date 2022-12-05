using System.ComponentModel.DataAnnotations;


namespace MysteryAuction.Infrastructure.Data.Models
{
    public class ProductCategory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DataConstraints.CategoryConstraints.MaxCategoryLength)]
        public string Category { get; set; } = null!;
    }
}
