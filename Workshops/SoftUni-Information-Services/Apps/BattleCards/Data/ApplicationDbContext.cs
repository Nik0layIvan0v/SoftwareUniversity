using BattleCards.Data.EntityModels;

namespace BattleCards.Data
{
    using Microsoft.EntityFrameworkCore;
    using static DatabaseConfiguration;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<UserCard> UserCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserCard>()
                .HasKey(x => new {x.CardId, x.UserId});

            modelBuilder
                .Entity<UserCard>()
                .HasOne(x => x.Card)
                .WithMany(x => x.UserCards)
                .HasForeignKey(x => x.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<UserCard>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserCards)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
