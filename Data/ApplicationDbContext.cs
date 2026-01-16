using Microsoft.EntityFrameworkCore;
using PracharSaarathi.Api.Models;

namespace PracharSaarathi.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Karyakarta> Karyakartas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed Admin (for demo purposes)
            // Password: Password123 (hashed)
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Name = "Demo Admin",
                PhoneNumber = "1234567890",
                PasswordHash = "AQAAAAEAACcQAAAAEHr8ZzG7Z6X6X/vX2v7Z8X6X/vX2v7Z8X6X/vX2v7Z8X6X/vX2v7Z8X6" // Placeholder hash
            });
        }
    }
}
