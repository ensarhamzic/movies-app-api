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

            modelBuilder.Entity<Movie>().HasKey(m => new { m.Id, m.CollectionId });
            modelBuilder.Entity<Movie>().Property(m => m.Id).ValueGeneratedNever();

            modelBuilder.Entity<Favorite>().HasKey(m => new { m.Id, m.UserId });
            modelBuilder.Entity<Favorite>().Property(m => m.Id).ValueGeneratedNever();

            modelBuilder.Entity<Publish>()
                .HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Publish>()
                .HasIndex(p => p.UserId).IsUnique();

            modelBuilder.Entity<PublishCollection>()
                .HasKey(pc => new { pc.PublishId, pc.CollectionId });

            modelBuilder.Entity<PublishCollection>()
                .HasOne<Collection>(pc => pc.Collection)
                .WithOne(c => c.Publish)
                .OnDelete(DeleteBehavior.NoAction);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Publish> Publishes { get; set; }
        public DbSet<PublishCollection> PublishedCollections { get; set; }

       
    }
}
