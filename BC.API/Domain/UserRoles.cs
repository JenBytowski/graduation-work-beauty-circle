using System;

namespace BC.API.Domain
{
  public static class UserRoles
  {
    public static string Client => "Client";
    public static string Master => "Master";

    public static bool Validate(string role)
    {
      return
        role == UserRoles.Client ||
        role == UserRoles.Master
      ;
    }
  }
}
