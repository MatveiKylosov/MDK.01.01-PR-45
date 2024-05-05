using API_Kylosov.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Kylosov.Context
{
    public class EmployeesContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public EmployeesContext()
        {
            Database.EnsureCreated();
            Employees.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1; uid=root;pwd=;database=ISP_21_2_10", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
