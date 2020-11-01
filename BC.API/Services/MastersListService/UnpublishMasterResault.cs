using System.Collections.Generic;

namespace BC.API.Services.MastersListService
{
  public class UnpublishMasterResault
  {
    public bool Result { get; set; }
    
    public IEnumerable<UnpublishMasterMessage> Messages { get; set; }
  }
  
  public class UnpublishMasterMessage
  {
    public string Text { get; set; }
  }
}
