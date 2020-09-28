using System.Collections.Generic;
using BC.API.Services.MastersListService;
using BC.API.Services.MastersListService.Data;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Controllers
{
  [ApiController]
  [Route("masters-list")]
  [ApiExplorerSettings(GroupName = "masters-list")]
  internal class MasterListController : ControllerBase
  {
    private readonly MasterListService _masterListerService;

    public MasterListController(MasterListService masterListerService)
    {
      _masterListerService = masterListerService;
    }

    [HttpGet]
    [Route("authenticate-by-vk")]
    public IEnumerable<MasterRes> GetMasters([FromQuery] MastersFilter filter)
    {
      return _masterListerService.GetMasters(filter);
    }
  }
}
