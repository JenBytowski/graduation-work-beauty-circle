namespace BC.API.Services.MasterListService.MasterContext
{
  public class MasterDTO
  {
    public string Name { get; set; }

    public static MasterDTO ParseаfromMastertoMasterDTO(Master master)
    {
      return new MasterDTO
      {
        Name = master.Name
      };
    }
  }
}
