using BC.API.Services.MasterListService.MasterContext;
using System.Collections.Generic;
using System.Linq;

namespace BC.API.Services.MasterListService
{
  public class MasterListService
  {
    readonly MasterContext.MasterContext _context;

    public MasterListService(MasterContext.MasterContext context)
    {
      _context = context;
    }

    public IEnumerable<MasterDTO> GetMasters()
    {
      return _context.Masters.Select(mstr => MasterDTO.ParseаfromMastertoMasterDTO(mstr));
    }
  }
}
