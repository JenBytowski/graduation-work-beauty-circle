using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.BalanceService.Data
{
  public class BalanceContext: DbContext
  {
    public BalanceContext(DbContextOptions<BalanceContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("balance");
      base.OnModelCreating(modelBuilder);
    }
  }
}
