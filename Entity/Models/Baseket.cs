using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Basket
    {
        [Key]
        [Column("BasketId")]
        public Guid BasketId { get; set; }

        public Guid UserId { get; set; }

        public int? Quantity { get; set; } //количество товара в корзине

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
