using System;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Services.BookingService.Controllers
{
  [ApiController]
  [Route("booking")]
  [ApiExplorerSettings(GroupName = "booking")]
  public class BookingController : ControllerBase
  {
    private BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
      _bookingService = bookingService;
    }

    [HttpGet]
    [Route("get-schedule")]
    public GetScheduleRes GetSchedule(Guid masterId)
    {
      return _bookingService.GetSchedule(masterId);
    }

    [HttpPost]
    [Route("add-working-week")]
    public void AddWorkingWeek(AddWorkingWeekReq request)
    {
      _bookingService.AddWorkingWeek(request);
    }

    [HttpPost]
    [Route("add-booking")]
    public void AddBooking(AddBookingReq request)
    {
      _bookingService.AddBooking(request);
    }

    [HttpPost]
    [Route("cancel-booking")]
    public void CancelBooking(CancelBookingReq request)
    {
      _bookingService.CancelBooking(request);
    }

    [HttpPost]
    [Route("add-pause")]
    public void AddPause(AddPauseReq request)
    {
      _bookingService.AddPause(request);
    }

    [HttpPost]
    [Route("cancel-pause")]
    public void CancelPause(CancelPauseReq request)
    {
      _bookingService.CancelPause(request);
    }
  }
}
