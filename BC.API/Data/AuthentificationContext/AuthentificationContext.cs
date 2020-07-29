using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Data.AuthentificationContext
{
    public class AuthentificationContext : IdentityDbContext
    {
        public AuthentificationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
