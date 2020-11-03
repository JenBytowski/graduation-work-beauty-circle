using System.Collections.Generic;

namespace BC.API.Services.MastersListService
{
  public class PublishMasterResult
  {
    public bool Result { get; set; }
    
    public IEnumerable<PublishMasterMessage> Messages { get; set; }
  }
  
  public class PublishMasterMessage
  {
    public string Text { get; set; }
  }
}
