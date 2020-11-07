using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC.API.Events;
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

    [HttpGet]
    [Route("get-master-by-id/{id}")]
    public MasterRes GetMasterById([FromRoute] Guid id)
    {
      return _mastersListerService.GetMasterById(id);
    }

    [HttpPost]
    [Route("publish-master/{id}")]
    public async Task<PublishMasterResult> PublishMaster([FromRoute] Guid id)
    {
      return await _mastersListerService.PublishMaster(id);
    }

    [HttpPost]
    [Route("unpublish-master/{id}")]
    public async Task<UnpublishMasterResault> UnpublishMaster([FromRoute] Guid id)
    {
      return await _mastersListerService.UnPublishMaster(id);
    }

    [HttpPost]
    [Route("update-master/{id}")]
    public async Task UpdateMaster([FromRoute] Guid id, UpdateMasterReq req)
    {
      await _mastersListerService.UpdateMasterInfo(id, req);
    }

    [HttpPost]
    [Route("upload-avatar/{id}")]
    public async Task<IActionResult> UploadAvatar([FromRoute] Guid id, IFormFile file)
    {
      this._mastersListerService.UploadAvatar(id, file.OpenReadStream(), file.FileName);
      return Ok();
    }
  }
}
