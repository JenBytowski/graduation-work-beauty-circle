using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BC.API.Services.BalanceService.Data
{
  public class BalanceContextFactory : IDesignTimeDbContextFactory<BalanceContext>
  {
    public BalanceContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<BalanceContext>();
      optionsBuilder
        // .UseSqlServer("Server=.;Database=BC;Trusted_Connection=True;MultipleActiveResultSets=true")
        .UseSqlServer("Server=.,5230;Database=BC;User Id=sa;Password=Password123;")
        ;

      return new BalanceContext(optionsBuilder.Options);
    }
  }
}
