using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using People_Power.Models;
using System.Reflection.Emit;

namespace People_Power.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin"},
                new Role { Id = 2, Name = "RegularUser"},
                new Role { Id = 3 , Name = "HRManager"}
                );                       
            // Precomputed hashed password for "Admin@123"[-
            var adminPasswordHash = "1234";

            var adminUser = new User
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@mail.com",
                RoleId = 1,
                PasswordHash = adminPasswordHash
            };            
            modelBuilder.Entity<User>().HasData(adminUser);
        }
    }  
}
