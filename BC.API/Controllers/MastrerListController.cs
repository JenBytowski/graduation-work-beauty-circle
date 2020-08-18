using System.Collections.Generic;
using BC.API.Services.MasterListService;
using BC.API.Services.MasterListService.MasterContext;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Controllers
{
  [Route("master-list")]
  [ApiController]
  public class MastrerListController : ControllerBase
  {
    private readonly MasterListService _masterListerService;

    public MastrerListController(MasterListService masterListerService)
    {
      _masterListerService = masterListerService;
    }

    [HttpGet]
    [Route("get-masters")]
    public IEnumerable<MasterDTO> GetMasters()
    {
      return _masterListerService.GetMasters();
    }
  }
}
