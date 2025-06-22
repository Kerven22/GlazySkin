using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Basket
    {
        [Key]
        [Column("BasketId")]
        public string BasketId { get; set; }

        public string Id { get; set; }
        [ForeignKey(nameof(Id))]
        public User User { get; set; }

        public int? Quantity { get; set; } = 0;//количество товара в корзине

        public List<Product>? Products { get; set; }
    }
}
