using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.AuthenticationService.AuthenticationContext
{
  public class AuthenticationContext : IdentityDbContext
  {
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
      Database.EnsureCreated();
    }
  }
}
