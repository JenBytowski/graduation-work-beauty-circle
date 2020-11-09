using System;
using System.Collections.Generic;
using System.IO.Pipelines;

namespace BC.API.Services.MastersListService.Data
{
  public class Master
  {
    public Guid Id { get; set; }

    public Guid? CityId { get; set; }

    public City City { get; set; }

    public string Name { get; set; }

    public string AvatarSourceFileName { get; set; }
    
    public string AvatarFileName { get; set; }
    
    public string ThumbnailFileName { get; set; }

    public string About { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string InstagramProfile { get; set; }

    public string VkProfile { get; set; }

    public string Viber { get; set; }

    public string Skype { get; set; }

    public Guid? SpecialityId { get; set; }
    
    public Speciality? Speciality { get; set; }

    public Guid PriceListId { get; set; }
    
    public PriceList PriceList { get; set; }

    public Guid ScheduleId { get; set; }
    
    public Schedule Schedule { get; set; }

    public double Stars { get; set; }

    public int ReviewsCount { get; set; }
    
    public bool IsPublish { get; set; }
  }

  public class City
  {
    public Guid Id { get; set; }

    public string MapProviderId { get; set; }

    public string Name { get; set; }
  }

  public class Schedule
  {
    public Guid Id { get; set; }

    public IList<ScheduleDay> Days { get; set; }
  }

  public class ScheduleDay
  {
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public IList<ScheduleDayItem> Items { get; set; }
  }

  public class ScheduleDayItem
  {
    public Guid Id { get; set; }
    public Guid ScheduleDayId { get; set; }
    public ScheduleDay ScheduleDay { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
  }

  public class Pause : ScheduleDayItem
  {
  }

  public class Booking : ScheduleDayItem
  {
  }

  public class Window : ScheduleDayItem
  {
  }

  public class Speciality
  {
    public Guid Id { get; set; }

    public string Name { get; set; }
  }

  public class ServiceType
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IList<Master> Masters { get; set; }

    public Guid ServiceTypeSubGroupId { get; set; }

    public ServiceTypeSubGroup ServiceTypeSubGroup { get; set; }
  }

  public class ServiceTypeGroup
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IList<ServiceTypeSubGroup> ServiceTypeSubGroupsGroups { get; set; }
  }

  public class ServiceTypeSubGroup
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ServiceTypeGroupId { get; set; }
    public ServiceTypeGroup ServiceTypeGroup { get; set; }
  }

  public class PriceList
  {
    public Guid Id { get; set; }

    public List<PriceListItem> PriceListItems { get; set; }
  }

  public class PriceListItem
  {
    public Guid Id { get; set; }

    public Guid PriceListId { get; set; }

    public PriceList PriceList { get; set; }

    public Guid ServiceTypeId { get; set; }

    public ServiceType ServiceType { get; set; }

    public int PriceMin { get; set; }

    public int PriceMax { get; set; }

    public int DurationInMinutesMax { get; set; }
  }
}
