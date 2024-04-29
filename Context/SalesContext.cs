using API_Kylosov.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Kylosov.Context
{
    public class SalesContext : DbContext
    {
        public DbSet<Sales> Sales { get; set; }
        public SalesContext()
        {
            Database.EnsureCreated();
            Sales.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1; uid=root;pwd=;database=CarDealership", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
