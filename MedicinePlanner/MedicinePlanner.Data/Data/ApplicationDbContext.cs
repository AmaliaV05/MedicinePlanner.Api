using MedicinePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicinePlanner.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<LoadingStock> LoadingStocks { get; set; }
        public DbSet<UnloadingStock> UnloadingStocks { get; set; }
        public DbSet<Planning> Plannings { get; set; }
        public DbSet<DailyPlanning> DailyPlannings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicine>()
                .HasOne(m => m.Stock)
                .WithOne(s => s.Medicine)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Stock>()
                .HasMany(s => s.UnloadingStocks)
                .WithOne(u => u.Stock)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Stock>()
                .HasMany(s => s.LoadingStocks)
                .WithOne(l => l.Stock)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Medicine>()
                .HasMany(m => m.Plannings)
                .WithOne(p => p.Medicine)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Planning>()
                .HasMany(p => p.DailyPlannings)
                .WithOne(d => d.Planning)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
