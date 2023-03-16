using Microsoft.EntityFrameworkCore;
using SmallShopApp.Entities;

namespace SmallShopApp.Data
{
    public class SmallShopAppDbContext : DbContext
    {
        public DbSet<ProductWeighted> Employees => Set<ProductWeighted>();
        public DbSet<ProductPacked> Managers => Set<ProductPacked>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("SmallShopAppDb");
        }
    }
}
