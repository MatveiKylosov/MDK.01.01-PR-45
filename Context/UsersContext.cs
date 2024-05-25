using API_Kylosov.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Kylosov.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public UsersContext()
        {
            Database.EnsureCreated();
            Users.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1; uid=ISP_21_2_10;pwd=DSFV988Np#;database=ISP_21_2_10", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
