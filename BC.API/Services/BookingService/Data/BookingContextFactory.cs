using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BC.API.Services.BookingService.Data
{
  public class BookingContextFactory : IDesignTimeDbContextFactory<BookingContext>
  {
    public BookingContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<BookingContext>();
      optionsBuilder
        // .UseSqlServer("Server=.;Database=BC;Trusted_Connection=True;MultipleActiveResultSets=true")
        .UseSqlServer("Server=.,5230;Database=BC;User Id=sa;Password=Password123;");

      return new BookingContext(optionsBuilder.Options);
    }
  }
}
