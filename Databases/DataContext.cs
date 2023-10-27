using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Databases
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Entities.User> Users { get; set; } = null!;
    }
}