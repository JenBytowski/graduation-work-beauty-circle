using System.Collections.Generic;
using BC.API.Services.MasterListService;
using BC.API.Services.MasterListService.MastersContext;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Controllers
{
  [Route("masters-list")]
  [ApiController]
  public class MastrerListController : ControllerBase
  {
    private readonly MasterListService _masterListerService;

    public MastrerListController(MasterListService masterListerService)
    {
      _masterListerService = masterListerService;
    }

    [HttpGet]
    public IEnumerable<MasterDTO> GetMasters()
    {
      return _masterListerService.GetMasters();
    }
  }
}
