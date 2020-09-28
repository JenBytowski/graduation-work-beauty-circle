using System.Collections.Generic;
using BC.API.Services.MastersListService;
using BC.API.Services.MastersListService.MastersContext;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Controllers
{
  [Route("masters-list")]
  [ApiController]
  internal class MasterListController : ControllerBase
  {
    private readonly MasterListService _masterListerService;

    public MasterListController(MasterListService masterListerService)
    {
      _masterListerService = masterListerService;
    }

    [HttpGet]
    [ApiExplorerSettings(GroupName = "master-list")]
    public IEnumerable<MasterRes> GetMasters([FromQuery] MastersFilter filter)
    {
      return _masterListerService.GetMasters(filter);
    }
  }
}
