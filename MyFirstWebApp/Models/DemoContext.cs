using Microsoft.EntityFrameworkCore;
using System;

namespace MyFirstWebApp.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<ShopModel> Shops { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\work\Temp\Shops.db");

    }
}
