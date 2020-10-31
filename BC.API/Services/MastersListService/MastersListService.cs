using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Domain;
using BC.API.Events;
using BC.API.Services.AuthenticationService.Data;
using BC.API.Services.MastersListService.Data;
using BC.API.Services.MastersListService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.MastersListService
{
  public class MastersListService
  {
    private readonly AuthenticationContext _authenticationContextContext;
    private readonly MastersContext _mastersContext;
    private readonly MastersListServiceConfig _config;
    private readonly HttpClient _httpClient;

    public MastersListService(MastersContext mastersContext, AuthenticationContext authenticationContextContext,  MastersListServiceConfig config, HttpClient httpClient)
    {
      _authenticationContextContext = authenticationContextContext;
      _mastersContext = mastersContext;
      _config = config;
      _httpClient = httpClient;
    }

    public async Task<IEnumerable<MasterRes>> GetMasters(MastersFilter filter)
    {
      return _mastersContext.Masters
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
        var master = _mastersContext.Masters.Include(mstr => mstr.PriceList)
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
      
      var master =   this._mastersContext.Masters.Single(m => m.Id == masterId);
      master.AvatarUrl = fileName;
    }

    //TODO отрефакторить логику и сделать валидацию
    public async Task UpdateMasterInfo(Guid masterId, UpdateMasterReq req)
    {
      try
      {
        var master = _mastersContext.Masters.Include(mstr => mstr.PriceList)
          .ThenInclude(mstr => mstr.PriceListItems).SingleOrDefault(mstr => mstr.Id == masterId);

        if (master == null)
        {
          throw new CantFindMasterException($"Cant find master by id: {masterId}");
        }

        master.Name = req.Name ?? master.Name;
        master.AvatarUrl = req.AvatarUrl ?? master.AvatarUrl;
        master.About = req.About ?? master.About;
        master.Address = req.Address ?? master.Address;
        master.Phone = req.Phone ?? master.Phone;
        master.InstagramProfile = req.InstagramProfile ?? master.InstagramProfile;
        master.VkProfile = req.VkProfile ?? master.VkProfile;
        master.Viber = req.Viber ?? master.Viber;
        master.Skype = req.Skype ?? master.Skype;
        master.SpecialityId = req.SpecialityId ?? master.SpecialityId;

        var newPriceListItems = req.PriceListItems.Where(rItm =>
          master.PriceList.PriceListItems.SingleOrDefault(itm => itm.Id != rItm.Id) != null);

        master.PriceList.PriceListItems.AddRange(newPriceListItems.Select(itm =>
          new PriceListItem
          {
            ServiceTypeId = itm.ServiceType.Id,
            PriceMax = itm.PriceMax,
            PriceMin = itm.PriceMin,
            DurationInMinutesMax = itm.DurationInMinutesMax
          }));

        await _mastersContext.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw new CantUpdateMasterException($"Cant update master by id: {masterId}", ex);
      }
    }

    public void Publish(Guid masterId)
    {
      var master = _mastersContext.Masters.Include(mstr => mstr.PriceList)
        .SingleOrDefault(mstr => mstr.Id == masterId);

      if (master == null)
      {
        throw new CantFindMasterException($"Cant find master by id: {masterId}");
      }
      
      
      
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

      var user = await this._authenticationContextContext.Users.SingleOrDefaultAsync(m => m.Id == @event.UserId);

      if (user == null || _mastersContext.Masters.SingleOrDefault(mstr => mstr.Id == @event.UserId) != null)
      {
        return;
      }

      this._mastersContext.Masters.Add(new Master
      {
        Id = @event.Id,
        Name = "No name",
        Schedule = new Schedule(),
        PriceList = new PriceList()
      });

      await this._mastersContext.SaveChangesAsync();
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

    private void ValidateMastersProfile(Master master)
    {
      if (string.IsNullOrEmpty(master.Name))
      {
        throw new ValidateMasterException($"Master {master.Id} name is null or empty");
      }

      if (string.IsNullOrEmpty(master.AvatarUrl))
      {
        throw new ValidateMasterException($"Master {master.Id} info is null or empty");
      }
      
      if (string.IsNullOrEmpty(master.Address))
      {
        throw new ValidateMasterException($"Master {master.Id} info is null or empty");
      }
      
      if (string.IsNullOrEmpty(master.Phone) && string.IsNullOrEmpty(master.VkProfile) && string.IsNullOrEmpty(master.InstagramProfile) && string.IsNullOrEmpty(master.Viber))
      {
        throw new ValidateMasterException($"Master {master.Id} info is null or empty");
      }
      
      
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
