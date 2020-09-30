using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using BC.API.Services.MastersListService.Data;

namespace BC.API.Services.MastersListService
{
  public class MasterListService
  {
    readonly Data.MastersContext _context;

    public MasterListService(Data.MastersContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<MasterRes>> GetMasters(MastersFilter filter)
    {
      return _context.Masters.Select(mstr => MasterRes.ParseFromMaster(mstr)).ToArray();
    }

    public MasterRes GetMasterById(Guid masterId)
    {
      return null;
    }

    public void UpdateMasterInfo(UpdateMasterReq req)
    {
      
    }

    public void Publish(Guid masterId)
    {
      
    }

    public void UnPublish(Guid masterId)
    {
      
    }
    
    public void OnUserCreated()
    {
      
    }
    
    public void OnUserDeleted()
    {
      
    }

    public void OnReviewPosted()
    {
      
    }
    
    public async Task ReorderMasters()
    {
    }
  }

  public class MastersFilter
  {
    public string CityId { get; set; }
    public Guid[] ServiceTypeIds { get; set; }
    public int? StartHour { get; set; }
    public int? EndHour { get; set; }
  }
}
