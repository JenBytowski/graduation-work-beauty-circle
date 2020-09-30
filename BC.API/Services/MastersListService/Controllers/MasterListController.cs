using System.Collections.Generic;
using System.Threading.Tasks;
using BC.API.Services.MastersListService;
using BC.API.Services.MastersListService.Data;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Controllers
{
  [ApiController]
  [Route("masters-list")]
  [ApiExplorerSettings(GroupName = "masters-list")]
  public class MasterListController : ControllerBase
  {
    private readonly MasterListService _masterListerService;

    public MasterListController(MasterListService masterListerService)
    {
      _masterListerService = masterListerService;
    }

    [HttpGet]
    [Route("get-masters")]
    public async Task<IEnumerable<MasterRes>> GetMasters([FromQuery] MastersFilter filter)
    {
      return await _masterListerService.GetMasters(filter);
    }
  }
}
