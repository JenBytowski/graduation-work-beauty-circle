using Microsoft.EntityFrameworkCore;

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
}
