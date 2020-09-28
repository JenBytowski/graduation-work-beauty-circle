using System;
using System.Collections.Generic;
using System.IO.Pipelines;

namespace BC.API.Services.MastersListService.Data
{
  internal class Master
  {
    public Guid Id { get; set; }

    public Guid CityId { get; set; }

    public City City { get; set; }

    public string Name { get; set; }

    public string AvatarUrl { get; set; }

    public string About { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string InstagramProfile { get; set; }

    public string VkProfile { get; set; }

    public string Viber { get; set; }

    public string Skype { get; set; }

    public Guid SpecialityId { get; set; }

    public Speciality Speciality { get; set; }

    // public List<Review> Reviews { get; set; }

    public PriceList PriceList { get; set; }

    public Schedule Schedule { get; set; }

    public double Stars { get; set; }

    public int ReviewsCount { get; set; }
  }

  internal class City
  {
    public Guid Id { get; set; }

    public string MapProviderId { get; set; }

    public string Name { get; set; }
  }

  internal class Schedule
  {
    public Guid Id { get; set; }

    public Guid MasterId { get; set; }

    public Master Master { get; set; }

    public IList<ScheduleDay> Days { get; set; }
  }

  internal class ScheduleDay
  {
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public IList<Pause> Pauses { get; set; }

    public IList<Booking> Bookings { get; set; }
  }

  internal class Pause
  {
    public Guid Id { get; set; }

    public Guid ScheduleDayId { get; set;}

    public ScheduleDay ScheduleDay { get; set;}

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
  }

  internal class Booking
  {
    public Guid Id { get; set;}

    public Guid MasterId { get; set;}

    public Guid ClientId { get; set;}

    public DateTime StartTime { get; set;}

    public DateTime EndTime { get; set; }

    public Guid ServiceTypeId { get; set; }

    public int Price { get; set; }
  }

  internal class Speciality
  {
    public Guid Id { get; set; }

    public string Name { get; set; }
  }

  internal class ServiceType
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IList<Master> Masters { get; set; }

    public Guid ServiceTypeGroupId { get; set; }

    public ServiceTypeGroup ServiceTypeGroup { get; set; }
  }

  internal class ServiceTypeGroup
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ParentServiceTypeGroupId { get; set; }

    public IList<ServiceTypeSubGroup> ServiceTypeSubGroupsGroups { get; set; }
  }

  internal class ServiceTypeSubGroup
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ServiceTypeGroup ServiceTypeGroup { get; set; }
  }

  internal class PriceList
  {
    public Guid Id { get; set; }

    public Guid MasterId { get; set; }

    public Master Master { get; set; }

    public List<PriceListItem> PriceListItems { get; set; }
  }

  internal class PriceListItem
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

