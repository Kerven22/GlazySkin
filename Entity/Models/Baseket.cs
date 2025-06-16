using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Basket
    {
        [Key]
        [Column("BasketId")]
        public Guid BasketId { get; set; }

        public string Id { get; set; }

        public User User { get; set; }

        public int? Quantity { get; set; } //количество товара в корзине

        public ICollection<Product>? Products { get; set; }
    }
}
