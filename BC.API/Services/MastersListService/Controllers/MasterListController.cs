using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC.API.Services.MastersListService.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerResponse(200, nameof(MasterRes), typeof(MasterRes))]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public IActionResult GetMasterById([FromRoute] Guid id)
    {
      try
      {
        return Ok(_mastersListerService.GetMasterById(id));
      }
      catch (CantFindMasterException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Code = APIErrorCodes.cant_find_master.ToString(),
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("publish-master/{id}")]
    [SwaggerResponse(200, nameof(PublishMasterResult), typeof(PublishMasterResult))]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public async Task<IActionResult> PublishMaster([FromRoute] Guid id)
    {
      try
      {
        return Ok(await _mastersListerService.PublishMaster(id));
      }
      catch (CantFindMasterException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Code = APIErrorCodes.cant_find_master.ToString(),
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [SwaggerResponse(200, nameof(UnpublishMasterResault), typeof(UnpublishMasterResault))]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    [Route("unpublish-master/{id}")]
    public async Task<IActionResult> UnpublishMaster([FromRoute] Guid id)
    {
      try
      {
        return Ok(await _mastersListerService.UnPublishMaster(id));
      }
      catch (CantFindMasterException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Code = APIErrorCodes.cant_find_master.ToString(),
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
    }

    [HttpPost]
    [Route("update-master/{id}")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400, nameof(BadAPIResponse), typeof(BadAPIResponse))]
    public async Task<IActionResult> UpdateMaster([FromRoute] Guid id, UpdateMasterReq req)
    {
      try
      {
        await _mastersListerService.UpdateMasterInfo(id, req);

        return Ok();
      }
      catch (CantUpdateMasterException ex)
      {
        return BadRequest(new BadAPIResponse
        {
          Code = APIErrorCodes.сant_update_master.ToString(),
          Messages = new List<string> {ex.Message, ex.InnerException?.Message}
        });
      }
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
