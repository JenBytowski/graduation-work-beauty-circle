using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC.API.Infrastructure.NswagClients.Booking;
using BC.API.Services.FeedbackService.Data;

namespace BC.API.Services.FeedbackService
{
  public class FeedbackService
  {
    private readonly FeedbackContext _context;
    private readonly BookingClient _bookingClient;

    public FeedbackService(FeedbackContext context, BookingClient bookingClient)
    {
      _context = context;
      _bookingClient = bookingClient;
    }
    
    public async Task PostBookingFeedback(PostBookingFeedbackReq req)
    {
    }

    public async Task<IEnumerable<BookingFeedbackRes>> GetBookingFeedbacks(GetBookingFeedbacksReq req)
    {
      return new[] {new BookingFeedbackRes()};
    }
    
  }

  public class GetBookingFeedbacksReq
  {
    public Guid BookingId { get; set; }
  }

  public class BookingFeedbackRes
  {
  }

  public class PostBookingFeedbackReq
  {
    public Guid BookingId { get; set; }
    public int Stars { get; set; }
    public string Text { get; set; }
  }
}
