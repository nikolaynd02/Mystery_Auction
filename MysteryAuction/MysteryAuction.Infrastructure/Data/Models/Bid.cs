using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryAuction.Infrastructure.Data.Models
{
    public class Bid
    {
        [ForeignKey(nameof(User))] 
        public string UserId { get; set; }
        public virtual MysteryAuctionUser User { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool HasWon { get; set; }
    }
}
