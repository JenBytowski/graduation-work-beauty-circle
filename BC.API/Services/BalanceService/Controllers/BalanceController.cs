using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Services.BalanceService.Controllers
{
  public class BalanceController: ControllerBase
  {
    private readonly BalanceService _balanceService;

    public BalanceController(BalanceService balanceService)
    {
      _balanceService = balanceService;
    }

    public async Task<IActionResult> IncreaseBalance()
    {
      var currentUserId = Guid.NewGuid(); // Надо красиво получить айдишку юзера как-то
      await this._balanceService.IncreaseBalance(currentUserId, 1);

      return Ok();
    }
  }
}
