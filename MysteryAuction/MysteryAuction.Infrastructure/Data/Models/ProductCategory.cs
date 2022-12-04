using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
