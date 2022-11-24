using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MysteryAuction.Data.Models
{
    public class MysteryAuctionUser : IdentityUser
    {
        [Required] 
        [MaxLength(DataConstraints.UserConstraints.MaxUsernameLength)]
        public string Username { get; set; } = null!;
    }
}
