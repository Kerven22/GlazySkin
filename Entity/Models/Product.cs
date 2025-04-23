using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Product
    {
        [Key]
        [Column("ProductId")]
        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }
        public string? Review { get; set; }

        public int Quantity { get; set; } //количество товара в магазине

        [InverseProperty(nameof(Comment.Product))]
        public ICollection<Comment>? Comments { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Cagetgory { get; set; }
    }
}
