using Microsoft.EntityFrameworkCore;

namespace StudentMVC.Models
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; } = null!;
  }
}