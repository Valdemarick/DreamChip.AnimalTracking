using DreamChip.AnimalTracking.Domain.Exceptions.Account;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

public abstract class BaseController : ControllerBase
{
    [NonAction]
    public IActionResult GetResponseFromResult<TValue>(Result<TValue> result)
    {
        return result.Match<IActionResult>(res =>
        {
            return Ok(res);
        }, ex =>
        {
            return ex switch
            {
                AccountAlreadyExistsException => BadRequest(),
                AccountNotFoundException => NotFound(),
                _ => Problem(statusCode: 500)
            };
        });
    }
}
