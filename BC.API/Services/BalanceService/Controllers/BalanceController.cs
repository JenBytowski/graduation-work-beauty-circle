using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BC.API.Services.BalanceService.Controllers
{
  [Route("balance")]
  public class BalanceController: ControllerBase
  {
    private readonly BalanceService _balanceService;

    public BalanceController(BalanceService balanceService)
    {
      _balanceService = balanceService;
    }

    [HttpPost]
    [Route("increase-balance")]
    public async Task<IActionResult> IncreaseBalance([FromHeader(Name = "UserId")] Guid userId)
    {
      await this._balanceService.IncreaseBalance(userId, 1);
      return Ok();
    }
  }
}
