using System.Collections.Generic;

namespace BC.API.Services.MastersListService
{
  internal class MasterCanBePublishedCheckResult
  {
    public bool Result { get; set; }
    
    public IList<MasterCanBePublishedCheckMessage> Messages { get; set; }
  }
  
  internal class MasterCanBePublishedCheckMessage
  {
    public string Text { get; set; }
  }
}
