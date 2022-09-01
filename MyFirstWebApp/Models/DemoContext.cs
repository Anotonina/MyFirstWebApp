using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;

namespace MyFirstWebApp.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<ShopModel> Shops { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning))
                .UseSqlite(@"Data Source=C:\work\Temp\Shops.db");

        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            string adminRoleName = "admin";
            string userRoleName = "user";
 
            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";
 
            
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName};
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            
            
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword };
            modelBuilder.Entity<User>().HasData(new User []{ adminUser });
            modelBuilder.Entity("RoleUser").HasData(new { RolesId = 1, UsersId = 1 });
            modelBuilder.Entity("RoleUser").HasData(new { RolesId = 2, UsersId = 1 });
        }  

    }
}
