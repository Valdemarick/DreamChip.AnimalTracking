using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DreamChip.AnimalTracking.WebApi.Filters;

internal sealed class AuthorizationFilter : IAuthorizationFilter
{
    private readonly IAccountRepository _accountRepository;

    public AuthorizationFilter(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers.Authorization.ToString();
        if (!authorizationHeader.StartsWith("Basic"))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
        }
    }
}
