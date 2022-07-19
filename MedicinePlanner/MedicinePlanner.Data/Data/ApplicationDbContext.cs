using Microsoft.EntityFrameworkCore;

namespace MedicinePlanner.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
