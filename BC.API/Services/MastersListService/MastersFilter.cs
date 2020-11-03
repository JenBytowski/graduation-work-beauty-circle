using System;

namespace BC.API.Services.MastersListService
{
  public class MastersFilter
  {
    public string CityId { get; set; }
    
    public Guid[] ServiceTypeIds { get; set; }
    
    public int? StartHour { get; set; }
    
    public int? EndHour { get; set; }
    
    public int Skip { get; set; }
    
    public int Take { get; set; }
  }
}
