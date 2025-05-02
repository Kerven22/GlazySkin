using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfigurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(k => k.CategoryId);
            builder.HasMany(k => k.Products)
                .WithOne(k => k.Cagetgory)
                .HasForeignKey(k => k.CategoryId);
        }
    }
}
