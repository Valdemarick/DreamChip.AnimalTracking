using DreamChip.AnimalTracking.Application.Dto.Account;
using DreamChip.AnimalTracking.Application.Services;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

public sealed class AccountsController : BaseController
{
    private readonly AccountService _accountService;

    public AccountsController(ILogger logger, AccountService accountService) : base(logger)
    {
        _accountService = accountService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAccountDto dto)
    {
        Logger.Debug($"AccountsController. CreateAsync. Started");

        var result = await _accountService.CreateAccountAsync(dto);

        Logger.Debug($"AccountsController. CreateAsync. Ended");

        return Response(result);
    }
}
