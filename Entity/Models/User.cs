using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class User : IdentityUser
    {

        //public string Id { get; set; }
        public string Login { get; set; }

        //public string PasswordHash { get; set; }

        //public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public string BasketId { get; set; }
        [ForeignKey(nameof(BasketId))]
        public Basket Basket { get; set; }

        [InverseProperty(nameof(Comment.User))]
        public ICollection<Comment> Comments { get; set; }

    }
}
