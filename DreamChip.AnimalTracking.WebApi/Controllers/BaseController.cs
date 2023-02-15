using DreamChip.AnimalTracking.Domain.Exceptions;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly ILogger Logger;

    protected BaseController(ILogger logger)
    {
        this.Logger = logger;
    }

    [NonAction]
    public IActionResult Response<TValue>(Result<TValue> result)
    {
        return result.Match<IActionResult>(res =>
        {
            Logger.Debug($"Controller. Response. Success");

            return Ok(res);
        }, ex =>
        {
            Logger.Debug($"Controller. Response. Failed. Exception type: {ex.GetType()}");

            return ex switch

            { 
                EntityAlreadyExistsException => BadRequest(),
                _ => Problem(statusCode: 500)
            };
        });
    }
}
