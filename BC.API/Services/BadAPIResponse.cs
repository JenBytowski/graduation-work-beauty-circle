using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace BC.API.Services
{
  public class BadAPIResponse
  {
    [SwaggerSchema("Error code")]
    public string Code { get; set; }
    
    [SwaggerSchema("Error messages")]
    public IEnumerable<string> Messages { get; set; }
  }

  public enum APIErrorCodes
  {
    booking_exception,
    cant_found_window_by_time_exception,
    authentication_exception,
    invalid_authentication_code_exception,
    сant_update_master_exception,
    cant_find_master_exception
  }
}
