using BC.API.Services.MastersListService.MastersContext;

namespace BC.API.Services.MastersListService
{
  internal class MasterRes
  {
    public string Name { get; set; }

    public static MasterRes ParseFromMaster(Master master)
    {
      return new MasterRes
      {
        Name = master.Name
      };
    }
  }
}
