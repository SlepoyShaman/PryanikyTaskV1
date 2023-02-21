using Microsoft.EntityFrameworkCore;
using PryanikyTaskV1.Models;

namespace PryanikyTaskV1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PryanikyDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Ball", Price = 100 },
                new Product { Id = 2, Name = "Boots", Price = 500},
                new Product { Id = 3, Name = "Socks", Price = 50 },
                new Product { Id = 4, Name = "Shorts", Price = 150}
                );
        }
    }
}
