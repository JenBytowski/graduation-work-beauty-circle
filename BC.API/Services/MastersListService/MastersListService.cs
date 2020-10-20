using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Threading.Tasks;
using BC.API.Events;
using BC.API.Services.MastersListService.Data;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.MastersListService
{
  public class MastersListService
  {
    readonly Data.MastersContext _context;
    private readonly MastersListServiceConfig _config;
    private readonly HttpClient _httpClient;

    public MastersListService(Data.MastersContext context, MastersListServiceConfig config, HttpClient httpClient)
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
        .Select(mstr => MasterRes.ParseFromMaster(mstr)).ToArray();
    }

    public MasterRes GetMasterById(Guid masterId)
    {
      return null;
    }

    public void UpdateMasterInfo(UpdateMasterReq req)
    {
    }

    public async void UploadAvatar(Guid masterId, Stream stream)
    {
      var formData = new MultipartFormDataContent();
      formData.Add(new StreamContent(stream), "random_file_name", "random_file_name");
      var req = new HttpRequestMessage(HttpMethod.Post, _config.FilesServiceUrl) {Content = formData};
      var fileName = await this._httpClient.SendAsync(req).Result.Content.ReadAsStringAsync();
      
      var master =  this._context.Masters.Single(m => m.Id == masterId);
      master.AvatarUrl = fileName;
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
  }
}
