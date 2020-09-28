using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BC.API.Services.AuthenticationService.Data
{
  public class AuthenticationContextFactory : IDesignTimeDbContextFactory<AuthenticationContext>
  {
    public AuthenticationContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<AuthenticationContext>();
      optionsBuilder.UseSqlServer("Server=.;Database=BC;Trusted_Connection=True;MultipleActiveResultSets=true");

      return new AuthenticationContext(optionsBuilder.Options);
    }
  }
}
