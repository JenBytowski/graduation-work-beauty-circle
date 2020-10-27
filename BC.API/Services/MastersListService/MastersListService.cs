using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Domain;
using BC.API.Events;
using BC.API.Services.MastersListService.Data;
using BC.API.Services.MastersListService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.MastersListService
{
  public class MastersListService
  {
    private readonly MastersContext _context;
    private readonly MastersListServiceConfig _config;
    private readonly HttpClient _httpClient;

    public MastersListService(MastersContext context, MastersListServiceConfig config, HttpClient httpClient)
    {
      _context = context;
      _config = config;
      _httpClient = httpClient;
    }

    public async Task<IEnumerable<MasterRes>> GetMasters(MastersFilter filter)
    {
      return _context.Masters
        .Include(mstr => mstr.PriceList)
        .ThenInclude(mstr => mstr.PriceListItems)
        .Include(mstr => mstr.Speciality)
        .Include(mstr => mstr.Schedule)
        .ThenInclude(mstr => mstr.Days)
        .ThenInclude(mstr => mstr.Items)
        .ThenInclude(mstr => mstr.ScheduleDay)
        .ThenInclude(mstr => mstr.Items)
        .Skip(filter.Skip)
        .Take(filter.Take)
        .Select(mstr => MasterRes.ParseFromMaster(mstr)).ToArray();
    }

    public MasterRes GetMasterById(Guid masterId)
    {
      try
      {
        var master = _context.Masters.Include(mstr => mstr.PriceList)
          .ThenInclude(mstr => mstr.PriceListItems)
          .Include(mstr => mstr.Speciality)
          .Include(mstr => mstr.Schedule)
          .ThenInclude(mstr => mstr.Days)
          .ThenInclude(mstr => mstr.Items)
          .ThenInclude(mstr => mstr.ScheduleDay)
          .ThenInclude(mstr => mstr.Items).Single(mstr => mstr.Id == masterId);

        return MasterRes.ParseFromMaster(master);
      }
      catch (Exception e)
      {
        throw new CantFindMasterException($"Cant find master by id: {masterId}");
      }
    }

    public async void UploadAvatar(Guid masterId, Stream stream)
    {
      var formData = new MultipartFormDataContent();
      formData.Add(new StreamContent(stream), "random_file_name", "random_file_name");
      var req = new HttpRequestMessage(HttpMethod.Post, _config.FilesServiceUrl) {Content = formData};
      var fileName = await this._httpClient.SendAsync(req).Result.Content.ReadAsStringAsync();
      
      var master =   this._context.Masters.Single(m => m.Id == masterId);
      master.AvatarUrl = fileName;
    }

    public void UpdateMasterInfo(UpdateMasterReq req)
    {
      var master = _context.Masters.Include(mstr => mstr.PriceList)
        .ThenInclude(mstr => mstr.PriceListItems).SingleOrDefault(mstr => mstr.Id == req.MasterId);

      if (master == null)
      {
        throw new CantFindMasterException($"Cant find master by id: {req.MasterId}");
      }
    }

    public void Publish(Guid masterId)
    {
    }

    public void UnPublish(Guid masterId)
    {
    }

    public void OnUserCreated()
    {
    }

    public async Task OnUserAssignedToRole(UserAssignedToRoleEvent @event)
    {
      if (@event.Role != UserRoles.Master)
      {
        return;
      }

      var master = await this._context.Masters.SingleOrDefaultAsync(m => m.Id == @event.UserId);

      if (master != null)
      {
        return;
      }

      this._context.Masters.Add(new Master
      {
        Id = @event.Id,
        Name = "No name",
      });

      await this._context.SaveChangesAsync();
    }

    public void OnUserDeleted()
    {
    }

    public void OnReviewPosted()
    {
    }

    public async Task ReorderMasters()
    {
    }

    public void OnScheduleDayChanged(ScheduleDayChangedEvent @event) // лучше не евент, а свой тип, правда он будет такой же тупо
    {
      throw new NotImplementedException();
    }
  }

  public class MastersListServiceConfig
  {
    public string FilesServiceUrl { get; set; }
  }

  public class MastersFilter
  {
    public string CityId { get; set; }
    public Guid[] ServiceTypeIds { get; set; }
    public int? StartHour { get; set; }
    public int? EndHour { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
  }
}
