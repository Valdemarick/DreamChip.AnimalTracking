using DreamChip.AnimalTracking.Application.Dto.Account;
using DreamChip.AnimalTracking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

public sealed class AuthenticationController : BaseController
{
    private readonly AccountService _accountService;

    public AuthenticationController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAccountDto dto)
    {
        var result = await _accountService.CreateAsync(dto);

        return GetResponseFromResult(result);
    }
}