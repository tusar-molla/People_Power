using Microsoft.EntityFrameworkCore;
using People_Power.Models;

namespace People_Power.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<CompanySettings> CompanySettings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "RegularUser" },
                new Role { Id = 3, Name = "HRManager" }
                );
            var adminPasswordHash = "123456";

            var adminUser = new User
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@mail.com",
                RoleId = 1,
                PasswordHash = adminPasswordHash
            };
            modelBuilder.Entity<User>().HasData(adminUser);

            // Seeding CompanySettings
            modelBuilder.Entity<CompanySettings>().HasData(
                new CompanySettings
                {
                    Id = 1, 
                    CompanyName = "People Power Inc.",
                    Address = "123 Business Street, City, Country",
                    PhoneNumber = "+123456789",
                    Email = "contact@peoplepower.com",
                    TaxNumber = "PP123456",
                    FiscalYearStart = new DateTime(2024, 1, 1),
                    FiscalYearEnd = new DateTime(2024, 12, 31),
                    StandardWorkingHours = 8
                });
        }
    }
}
