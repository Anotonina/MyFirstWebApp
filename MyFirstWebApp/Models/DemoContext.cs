﻿using Microsoft.EntityFrameworkCore;
using System;

namespace MyFirstWebApp.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<ShopModel> Shops { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\work\Temp\Shops.db");

        internal object GetCashier(int v)
        {
            throw new NotImplementedException();
        }
    }
}
