using System;
using System.Collections.Generic;
using System.Net;
using BC.API.Services.BookingService.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerResponse(200, nameof(GetScheduleRes), typeof(GetScheduleRes))]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    [SwaggerResponse(500, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public IActionResult GetSchedule(Guid masterId)
    {
      try
      {
        var schedule = _bookingService.GetSchedule(masterId);

        return Ok(schedule);
      }
      catch (BookingException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("add-working-week")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    [SwaggerResponse(500, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public IActionResult AddWorkingWeek(AddWorkingWeekReq request)
    {
      try
      {
        _bookingService.AddWorkingWeek(request);

        return Ok();
      }
      catch (BookingException ex)
      {
        return BadRequest(new BadAPIResponse {Messages = new List<string> {ex.Message, ex.InnerException?.Message}});
      }
    }

    [HttpPost]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    [SwaggerResponse(500, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    [Route("add-booking")]
    public IActionResult AddBooking(AddBookingReq request)
    {
      try
      {
        _bookingService.AddBooking(request);

        return Ok();
      }
      catch (BookingException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("cancel-booking")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    [SwaggerResponse(500, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public IActionResult CancelBooking(CancelBookingReq request)
    {
      try
      {
        _bookingService.CancelBooking(request);
        
        return Ok();
      }
      catch (BookingException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("add-pause")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    [SwaggerResponse(500, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public IActionResult AddPause(AddPauseReq request)
    {
      try
      {
        _bookingService.AddPause(request);

        return Ok();
      }
      catch (BookingException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("cancel-pause")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    [SwaggerResponse(500, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public IActionResult CancelPause(CancelPauseReq request)
    {
      try
      {
        _bookingService.CancelPause(request);

        return Ok();
      }
      catch (BookingException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }
  }
}
