﻿using API_Kylosov.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Kylosov.Context
{
    public class CarsContext : DbContext
    {
        public DbSet<Cars> Cars { get; set; }
        public CarsContext()
        {
            Database.EnsureCreated();
            Cars.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1; uid=root;pwd=;database=CarDealership", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
