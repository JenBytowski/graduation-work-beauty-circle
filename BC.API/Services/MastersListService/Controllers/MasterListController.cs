using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Services.MastersListService.Controllers
{
  [ApiController]
  [Route("masters-list")]
  [ApiExplorerSettings(GroupName = "masters-list")]
  public class MasterListController : ControllerBase
  {
    private readonly MastersListService _mastersListerService;

    public MasterListController(MastersListService mastersListerService)
    {
      _mastersListerService = mastersListerService;
    }

    [HttpGet]
    public async Task<IEnumerable<MasterRes>> GetMasters([FromQuery] MastersFilter filter)
    {
      return await _mastersListerService.GetMasters(filter);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task UpdateMaster()
    {
      
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> UploadAvatar([FromRoute] Guid id, IFormFile file)
    {
      this._mastersListerService.UploadAvatar(id, file.OpenReadStream());
      return Ok();
    }
  }
}
