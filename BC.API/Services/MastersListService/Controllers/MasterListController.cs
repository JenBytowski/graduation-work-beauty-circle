using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC.API.Services.MastersListService;
using BC.API.Services.MastersListService.Data;
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
    [Route("get-masters")]
    public async Task<IEnumerable<MasterRes>> GetMasters([FromQuery] MastersFilter filter)
    {
      return await _mastersListerService.GetMasters(filter);
    }

    [HttpPost]
    [Route("{id}/avatar")]
    public async Task<IActionResult> UploadAvatar([FromRoute] Guid id, IFormFile file)
    {
      if (file.Length <= 0)
      {
        return BadRequest();
      }

      await this._masterListerService.UploadMasterAvatar(id, file.OpenReadStream());

      return Ok();
    }
  }
}
