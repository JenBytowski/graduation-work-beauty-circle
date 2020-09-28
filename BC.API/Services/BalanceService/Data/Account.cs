using System;

namespace BC.API.Services.BalanceService.Data
{
  public class Account
  {
    public Guid Id { get; set; }
    public Guid HolderId { get; set; }
    public int Balance { get; set; }
  }
}
