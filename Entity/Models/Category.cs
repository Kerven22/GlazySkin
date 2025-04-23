using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Category
    {
        [Key]
        [Column("CategoryId ")]
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        [InverseProperty(nameof(Product.Cagetgory))]
        public ICollection<Product>? Products { get; set; }
    }
}
