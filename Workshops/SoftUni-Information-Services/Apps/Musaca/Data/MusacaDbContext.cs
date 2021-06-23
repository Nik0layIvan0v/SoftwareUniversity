using Microsoft.EntityFrameworkCore;
using Musaca.Data.EntityModels;

namespace Musaca.Data
{
    public class MusacaDbContext : DbContext, IDatabaseProvider
    {
        public MusacaDbContext()
        {
        }

        public MusacaDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Order> Orders { get; set; }
        //DBSets!

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Musaca;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}