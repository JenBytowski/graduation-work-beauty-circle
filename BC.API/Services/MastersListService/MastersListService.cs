using System;
using System.Collections.Generic;
using System.Linq;
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

    public MastersListService(Data.MastersContext context)
    {
      _context = context;
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

  public class MastersFilter
  {
    public string CityId { get; set; }
    public Guid[] ServiceTypeIds { get; set; }
    public int? StartHour { get; set; }
    public int? EndHour { get; set; }
  }
}
