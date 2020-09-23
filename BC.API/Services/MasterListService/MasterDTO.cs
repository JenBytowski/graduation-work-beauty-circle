using BC.API.Services.MasterListService.MastersContext;

namespace BC.API.Services.MasterListService
{
  public class MasterDTO
  {
    public string Name { get; set; }

    public static MasterDTO ParseFromMaster(Master master)
    {
      return new MasterDTO
      {
        Name = master.Name
      };
    }
  }
}
