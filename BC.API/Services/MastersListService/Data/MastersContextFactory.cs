using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BC.API.Services.MastersListService.Data
{
  internal class MastersContextFactory : IDesignTimeDbContextFactory<MastersContext>
  {
    public MastersContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<MastersContext>();
      optionsBuilder
        // .UseSqlServer("Server=.;Database=BC;Trusted_Connection=True;MultipleActiveResultSets=true")
        .UseSqlServer("Server=.,5230;Database=BC;User Id=sa;Password=Password123;")
        ;

      return new MastersContext(optionsBuilder.Options); }
  }
}
