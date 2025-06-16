using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entity.Models
{
    public class User:IdentityUser
    {
        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Basket Basket { get; set; }

        [InverseProperty(nameof(Comment.User))]
        public ICollection<Comment> Comments { get; set; }

    }
}
