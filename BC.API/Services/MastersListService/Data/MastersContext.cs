using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BC.API.Services.MastersListService.Data
{
  internal class MastersContext : DbContext
  {
    public DbSet<Master> Masters { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<ScheduleDay> ScheduleDays { get; set; }

    public DbSet<Pause> Pauses { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Speciality> Specialities { get; set; }

    public DbSet<ServiceType> ServiceTypes { get; set; }

    public DbSet<ServiceTypeGroup> ServiceTypeGroups { get; set; }

    public DbSet<PriceList> PriceLists { get; set; }

    public DbSet<PriceListItem> PriceListItems { get; set; }

    public MastersContext(DbContextOptions<MastersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("masters");
      base.OnModelCreating(modelBuilder);
    }
  }
  
  internal class MastersContextFactory : IDesignTimeDbContextFactory<MastersContext>
  {
    public MastersContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<MastersContext>();
      optionsBuilder
        // .UseSqlServer("Server=.;Database=BC;Trusted_Connection=True;MultipleActiveResultSets=true")
        .UseSqlServer("Server=.,5008;Database=BC;User Id=sa;Password=Password123;")
      ;

      return new MastersContext(optionsBuilder.Options); }
  }
}
