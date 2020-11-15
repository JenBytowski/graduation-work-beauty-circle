using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.FeedbackService.Data
{
  public class FeedbackContext: DbContext
  {
    public DbSet<BookingFeedback> BookingFeedbacks { get; set; }
    
    public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.HasDefaultSchema("feedback");
      base.OnModelCreating(builder);
    }
  }

  public class BookingFeedback
  {
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid ClientId { get; set; }
    public string ClientName { get; set; }
    public Guid MasterId { get; set; }
    public string MasterName { get; set; }
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; }
    public DateTimeOffset PostedAt { get; set; }
    public int Stars { get; set; }
    public string Text { get; set; }
  }
  

}
