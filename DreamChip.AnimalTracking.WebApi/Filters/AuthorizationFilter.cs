using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace DreamChip.AnimalTracking.WebApi.Filters;

internal sealed class AuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IAccountRepository _accountRepository;

    public AuthorizationFilter(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers.Authorization.ToString();
        if (!authorizationHeader.StartsWith("Basic"))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }

        var tuple = GetEmailAndPasswordFromHeader(authorizationHeader);

        var user = await _accountRepository.GetByEmailAsync(tuple.email);
        if (user is null)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
        }
    }

    private (string email, string password) GetEmailAndPasswordFromHeader(string headerValue)
    {
        var encodedValue = headerValue.Substring("Basic".Length).Trim();
        var decodedValue = Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(encodedValue)).Split(':');

        return (decodedValue[0], decodedValue[1]);
    }
}
