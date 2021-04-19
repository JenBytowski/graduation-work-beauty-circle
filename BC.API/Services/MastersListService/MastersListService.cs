using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Domain;
using BC.API.Events;
using BC.API.Infrastructure.NswagClients.Booking;
using BC.API.Infrastructure.Impl;
using BC.API.Services.MastersListService.Data;
using BC.API.Services.MastersListService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BC.API.Services.MastersListService
{
  public class MastersListService
  {
    private readonly MastersContext _mastersContext;
    private readonly AvatarImageProcessingSaga _avatarImageProcessingSaga;
    private readonly FilesServiceClient _filesServiceClient;
    private readonly HttpClient _httpClient;
    private readonly BookingClient _bookingClient;

    public MastersListService(
      MastersContext mastersContext,
      AvatarImageProcessingSaga avatarImageProcessingSaga,
      FilesServiceClient filesServiceClient,
      HttpClient httpClient,
      BookingClient bookingClient
    )
    {
      _avatarImageProcessingSaga = avatarImageProcessingSaga;
      _mastersContext = mastersContext;
      _filesServiceClient = filesServiceClient;
      _httpClient = httpClient;
      _bookingClient = bookingClient;
    }

    public async Task<IEnumerable<MasterRes>> GetMasters(MastersFilter filter)
    {
      var res = _mastersContext.Masters
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
        .ToArray()
        .Select(mstr =>
        {
          var masterRes = MasterRes.ParseFromMaster(mstr);

          var avatarFileName = mstr.AvatarFileName ?? mstr.AvatarSourceFileName;
          if (!string.IsNullOrWhiteSpace(avatarFileName))
          {
            masterRes.AvatarUrl = _filesServiceClient.BuildPublicUrl(avatarFileName);
          }

          var thumbnailFileName = mstr.ThumbnailFileName ?? mstr.AvatarFileName ?? mstr.AvatarSourceFileName;
          if (!string.IsNullOrWhiteSpace(thumbnailFileName))
          {
            masterRes.ThumbnailUrl = _filesServiceClient.BuildPublicUrl(thumbnailFileName);
          }

          return masterRes;
        }).ToArray();

      return res;
    }

    public MasterRes GetMasterById(Guid masterId)
    {
      var master = _mastersContext.Masters.Include(mstr => mstr.PriceList)
          .ThenInclude(mstr => mstr.PriceListItems)
          .Include(mstr => mstr.Speciality)
          .Include(mstr => mstr.Schedule)
          .ThenInclude(mstr => mstr.Days)
          .ThenInclude(mstr => mstr.Items)
          .ThenInclude(mstr => mstr.ScheduleDay)
          .ThenInclude(mstr => mstr.Items).SingleOrDefault(mstr => mstr.Id == masterId);

      if (master == null)
      {
        throw new CantFindMasterException($"Cant find master by id: {masterId}");
      }

      return MasterRes.ParseFromMaster(master);
    }

    public async void UploadAvatar(Guid masterId, Stream stream, string fileName)
    {
      var master = this._mastersContext.Masters.Single(m => m.Id == masterId);

      var name = Path.Combine("masters", masterId.ToString(), "avatarSource" + Path.GetExtension(fileName))
        .Replace(@"\", @"/");
      await _filesServiceClient.PostFile(stream, fileName);

      master.AvatarSourceFileName = name;

      this._mastersContext.SaveChanges();
      this._avatarImageProcessingSaga.Start(masterId);
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
        master.About = req.About ?? master.About;
        master.Address = req.Address ?? master.Address;
        master.Phone = req.Phone ?? master.Phone;
        master.InstagramProfile = req.InstagramProfile ?? master.InstagramProfile;
        master.VkProfile = req.VkProfile ?? master.VkProfile;
        master.Viber = req.Viber ?? master.Viber;
        master.Skype = req.Skype ?? master.Skype;
        master.SpecialityId = req.SpecialityId ?? master.SpecialityId;

        if (req.PriceListItems != null)
        {
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
        }

        await _mastersContext.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw new CantUpdateMasterException($"Cant update master by id: {masterId}", ex);
      }
    }

    public async Task<PublishMasterResult> PublishMaster(Guid masterId)
    {
      var master = _mastersContext.Masters.Include(mstr => mstr.PriceList)
        .SingleOrDefault(mstr => mstr.Id == masterId);

      if (master == null)
      {
        throw new CantFindMasterException($"Cant find master by id: {masterId}");
      }

      if (master.IsPublish)
      {
        return new PublishMasterResult
        {
          Messages = new List<PublishMasterMessage> { new PublishMasterMessage { Text = "Master has already published" } }
        };
      }

      var validateResult = CheckIfMasterCanBePublished(master);

      if (!validateResult.Result)
      {
        return new PublishMasterResult
        {
          Messages = validateResult.Messages.Select(msg =>
            new PublishMasterMessage { Text = msg.Text })
        };
      }

      master.IsPublish = true;
      await _mastersContext.SaveChangesAsync();

      return new PublishMasterResult { Result = true };
    }

    public async Task<UnpublishMasterResault> UnPublishMaster(Guid masterId)
    {
      var master = _mastersContext.Masters.Include(mstr => mstr.PriceList)
        .SingleOrDefault(mstr => mstr.Id == masterId);

      if (master == null)
      {
        throw new CantFindMasterException($"Cant find master by id: {masterId}");
      }

      if (!master.IsPublish)
      {
        return new UnpublishMasterResault
        {
          Messages = new List<UnpublishMasterMessage>
          {
            new UnpublishMasterMessage {Text = "Master has already unpublished"}
          }
        };
      }

      master.IsPublish = default;
      await _mastersContext.SaveChangesAsync();

      return new UnpublishMasterResault { Result = true };
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

      this._mastersContext.Masters.Add(new Master
      {
        Id = @event.UserId,
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

    private MasterCanBePublishedCheckResult CheckIfMasterCanBePublished(Master master)
    {
      var result = new MasterCanBePublishedCheckResult
      {
        Messages = new List<MasterCanBePublishedCheckMessage>(),
        Result = true
      };

      if (string.IsNullOrEmpty(master.Name))
      {
        result.Messages.Add(new MasterCanBePublishedCheckMessage { Text = "Master name is empty" });
      }

      if (string.IsNullOrEmpty(master.AvatarSourceFileName))
      {
        result.Messages.Add(new MasterCanBePublishedCheckMessage { Text = "Master avatar is empty" });
      }

      if (string.IsNullOrEmpty(master.Address))
      {
        result.Messages.Add(new MasterCanBePublishedCheckMessage { Text = "Master info is empty" });
      }

      if (string.IsNullOrEmpty(master.Phone) && string.IsNullOrEmpty(master.VkProfile) && string.IsNullOrEmpty(master.InstagramProfile)
          && string.IsNullOrEmpty(master.Viber) && string.IsNullOrEmpty(master.Skype))
      {
        result.Messages.Add(new MasterCanBePublishedCheckMessage { Text = "Master contacts is empty" });
      }

      if (master.Speciality == null)
      {
        result.Messages.Add(new MasterCanBePublishedCheckMessage { Text = "Master speciality is empty" });
      }

      if (result.Messages.Count > 0)
      {
        result.Result = default;
      }

      return result;
    }
  }
}
