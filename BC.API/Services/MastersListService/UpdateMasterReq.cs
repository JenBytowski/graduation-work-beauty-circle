using System;
using System.Collections.Generic;
using BC.API.Services.MastersListService.Data;

namespace BC.API.Services.MastersListService
{
  public class UpdateMasterReq
  {
    public string Name { get; set; }

    public string AvatarUrl { get; set; }

    public string About { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string InstagramProfile { get; set; }

    public string VkProfile { get; set; }

    public string Viber { get; set; }

    public string Skype { get; set; }
    
    public Guid? SpecialityId { get; set; }
    
    public IEnumerable<PriceListItemReq> PriceListItems { get; set; }
  }
  
  public class PriceListItemReq
  {
    public Guid Id { get; set; }
    
    public ServiceTypeReq ServiceType { get; set; }

    public int PriceMin { get; set; }

    public int PriceMax { get; set; }

    public int DurationInMinutesMax { get; set; }

    public static PriceListItemReq ParseFromPriseListItem(PriceListItem priceListItem)
    {
      return new PriceListItemReq
      {
        Id = priceListItem.Id,
        ServiceType = ServiceTypeReq.ParseFromServiceType(priceListItem.ServiceType),
        PriceMin = priceListItem.PriceMin,
        PriceMax = priceListItem.PriceMax,
        DurationInMinutesMax = priceListItem.DurationInMinutesMax
      };
    }
  }
  
  public class ServiceTypeReq
  {
    public Guid Id { get; set; }
    
    public Guid ServiceTypeSubGroupId { get; set; }
    
    public static ServiceTypeReq ParseFromServiceType(ServiceType serviceType)
    {
      return new ServiceTypeReq
      {
        Id = serviceType.Id,
        ServiceTypeSubGroupId = serviceType.ServiceTypeSubGroupId
      };
    }
  }
}
