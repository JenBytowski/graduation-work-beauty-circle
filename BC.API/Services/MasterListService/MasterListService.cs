using System.Collections.Generic;
using System.Linq;
using BC.API.Services.MasterListService.MastersContext;

namespace BC.API.Services.MasterListService
{
  public class MasterListService
  {
    readonly MastersContext.MastersContext _context;

    public MasterListService(MastersContext.MastersContext context)
    {
      _context = context;
    }

    public IEnumerable<MasterDTO> GetMasters()
    {
      return _context.Masters.Select(mstr => MasterDTO.ParseFromMastertoMasterDTO(mstr));
    }
  }
}
