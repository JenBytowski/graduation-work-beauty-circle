using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC.API.Services;
using BC.API.Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BC.API.Controllers
{
  [Route("authentication")]
  [ApiExplorerSettings(GroupName = "authentication")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("authenticate-by-vk")]
    [SwaggerResponse(200, nameof(AuthenticationResponse), typeof(AuthenticationResponse))]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public async Task<IActionResult> AuthenticatebyVK(AuthenticationCodeRequest request)
    {
      try
      {
        var result = await _authenticationService.AuthenticateByVK(request.Code, request.RedirectUrl, request.Role);

        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("authenticate-by-instagram")]
    [SwaggerResponse(200, nameof(AuthenticationResponse), typeof(AuthenticationResponse))]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public async Task<IActionResult> AuthenticatebyInstagram(AuthenticationCodeRequest request)
    {
      try
      {
        var result = await _authenticationService.AuthenticateByInstagram(request.Code, request.RedirectUrl, request.Role);
        
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("authenticate-by-google")]
    [SwaggerResponse(200, nameof(AuthenticationResponse), typeof(AuthenticationResponse))]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public async Task<IActionResult> AuthenticatebyGoogle(AuthenticationCodeRequest request)
    {
      try
      {
        var result = await _authenticationService.AuthenticateByGoogle(request.Code, request.RedirectUrl, request.Role);
        
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("authenticate-by-phone-step-1")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public async Task<IActionResult> AuthenticatebyPhoneStep1(AuthenticationPhoneRequest request)
    {
      try
      {
        await _authenticationService.AuthenticateByPhoneStep1(request.Phone, request.Role);
        
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("authenticate-by-phone-step-2")]
    [SwaggerResponse(200, nameof(AuthenticationResponse), typeof(AuthenticationResponse))]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public async Task<IActionResult> AuthenticatebyPhoneStep2(AuthenticatebyPhoneStep2Req req)
    {
      try
      {
        var result = await _authenticationService.AuthenticateByPhoneStep2(req);

        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }
  }
}
