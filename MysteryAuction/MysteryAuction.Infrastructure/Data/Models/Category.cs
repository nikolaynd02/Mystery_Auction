using System.ComponentModel.DataAnnotations;


namespace MysteryAuction.Infrastructure.Data.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DataConstraints.CategoryConstraints.MaxNameLength)]
        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
