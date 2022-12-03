using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryAuction.Infrastructure.Data.Models
{
    public class Bid
    {
        [Key]
        public Guid Id { get; set; }

        [Required] 
        public string UserId { get; set; } = null!;

        [Required]
        public string ProductId { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public bool HasWon { get; set; }
    }
}
