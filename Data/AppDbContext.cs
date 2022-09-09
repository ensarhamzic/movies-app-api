using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Movies_API.Data.Models;

namespace Movies_API.Data
{
    public class AppDbContext : DbContext
    {
        private IConfiguration configuration;

        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }

       
    }
}
