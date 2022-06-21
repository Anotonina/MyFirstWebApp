using Microsoft.EntityFrameworkCore;

namespace MyFirstWebApp.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<ShopModel> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\work\Temp\Shops.db");
    }
}
