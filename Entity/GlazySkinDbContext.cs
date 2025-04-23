using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity
{
    public class GlazySkinDbContext:DbContext
    {
        public GlazySkinDbContext(DbContextOptions<GlazySkinDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<Comment> Comments { get; set; }

    }
}
