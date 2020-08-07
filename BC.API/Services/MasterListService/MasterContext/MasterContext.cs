using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.MasterListService.MasterContext
{
  public class MasterContext : DbContext
  {
    public MasterContext(DbContextOptions<MasterContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Master> Masters { get; set; }
  }
}
