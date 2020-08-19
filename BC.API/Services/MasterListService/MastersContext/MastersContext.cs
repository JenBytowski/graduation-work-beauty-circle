using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.MasterListService.MastersContext
{
  public class MastersContext : DbContext
  {
    public MastersContext(DbContextOptions<MastersContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Master> Masters { get; set; }
  }
}
