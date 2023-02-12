using DreamChip.AnimalTracking.Application.Dto.Account;
using DreamChip.AnimalTracking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AccountsController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountsController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAccountDto dto)
    {
        var result = await _accountService.CreateAccountAsync(dto);

        return Ok(result);
    }
}