using DreamChip.AnimalTracking.Application.Dto.Account;
using DreamChip.AnimalTracking.Application.Services;
using DreamChip.AnimalTracking.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[Route("[controller]")]
[ServiceFilter(typeof(AuthorizationFilter))]
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

    [HttpGet("search")]
    public async Task<IActionResult> GetPage([FromQuery] AccountPageRequestDto dto)
    {
        var result = await _accountService.GetPageAsync(dto);

        return GetResponseFromResult(result);
    }
}
