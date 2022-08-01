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
    }
}
