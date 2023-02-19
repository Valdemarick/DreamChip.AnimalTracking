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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int accountId)
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var result = await _accountService.DeleteAsync(id);

        return GetResponseFromResult(result);
    }
}
