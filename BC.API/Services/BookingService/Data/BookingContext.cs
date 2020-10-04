using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.BookingService.Data
{
  public class BookingContext : DbContext
  {
    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<ScheduleDay> ScheduleDays { get; set; }

    public  DbSet<ScheduleDayItem> ScheduleDayItems { get; set; }

    public BookingContext(DbContextOptions<BookingContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.HasDefaultSchema("booking");
      base.OnModelCreating(builder);
    }
  }

  public class Schedule
  {
    public Guid Id { get; set; }
    public Guid MasterId { get; set; }
    public IList<ScheduleDay> Days { get; set; }
    
    public bool ConnectedBookingsPrefered { get; set; }
    
    public bool PreferedGapInMinutes { get; set; }
  }

  public class ScheduleDay
  {
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public IList<ScheduleDayItem> Items { get; set; }
  }

  public class ScheduleDayItem
  {
    public Guid Id { get; set; }
    public Guid ScheduleDayId { get; set; }
    public ScheduleDay ScheduleDay { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
  }

  public class Pause : ScheduleDayItem
  {
    public string Description { get; set; }
  }

  public class Booking : ScheduleDayItem
  {
    public Guid ClientId { get; set; }
    public Guid ServiceTypeId { get; set; }
    public string ServiceTypeName { get; set; }
    public string Description { get; set; }
    public int PriceMin { get; set; }
    public int PriceMax { get; set; }
    public int DurationInMinutesMax { get; set; }
  }

  public class Window : ScheduleDayItem
  {
  }
}
