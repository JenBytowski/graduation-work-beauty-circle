using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace BC.API.Services
{
  public class BadAPIResponse
  {
    [SwaggerSchema("Error messages")]
    public IEnumerable<string> Messages { get; set; }
  }
}
