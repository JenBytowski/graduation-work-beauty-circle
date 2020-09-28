using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.MastersListService.MastersContext
{
  internal class MastersContext : DbContext
  {
    public MastersContext(DbContextOptions<MastersContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Master> Masters { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("masters");
      base.OnModelCreating(modelBuilder);
    }
  }
}
