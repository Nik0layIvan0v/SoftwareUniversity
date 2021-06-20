using Git.Data.Models;

namespace Git.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<Commit> Commits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Git;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commit>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Commits)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Commit>()
                .HasOne(x => x.Repository)
                .WithMany(x => x.Commits)
                .HasForeignKey(x => x.RepositoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}