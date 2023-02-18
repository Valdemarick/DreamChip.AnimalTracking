using DreamChip.AnimalTracking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[Route("[controller]")]
public sealed class AccountsController : BaseController
{
    private readonly AccountService _accountService;

    public AccountsController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("{accountId:long}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long accountId)
    {
        if (accountId <= 0)
        {
            return BadRequest();
        }

        var result = await _accountService.GetByIdAsync(accountId);

        return GetResponseFromResult(result);
    }
}
