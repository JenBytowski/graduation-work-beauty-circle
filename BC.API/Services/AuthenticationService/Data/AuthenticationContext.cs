using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.AuthenticationService.Data
{
  public class AuthenticationContext : IdentityDbContext<User, Role, Guid>
  {
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.HasDefaultSchema("auth");
      base.OnModelCreating(builder);
    }
  }
}
