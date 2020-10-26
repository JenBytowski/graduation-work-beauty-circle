using System;
using System.Linq;

namespace BC.API.Domain
{
  public static class UserRoles
  {
    public static string Client => "Client";
    public static string Master => "Master";

    public static string[] AllRoles => new[]
    {
      UserRoles.Client, 
      UserRoles.Master
    };
    
    public static bool Validate(string role)
    {
      return UserRoles.AllRoles.Any(r => r == role);
    }
  }
}
