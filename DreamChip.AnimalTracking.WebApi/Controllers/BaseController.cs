using DreamChip.AnimalTracking.Domain.Exceptions.Account;
using DreamChip.AnimalTracking.Domain.Exceptions.Location;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private static readonly List<Type> BadRequestExceptionTypes = new List<Type>
    {
        typeof(AccountLinkedWithAnimalsException),
        typeof(LocationLinkedWithAnimalException),
    };

    private static readonly List<Type> NotFoundExceptionTypes = new List<Type>
    {
        typeof(AccountNotFoundException),
        typeof(LocationNotFoundException),
    };
    
    private static readonly List<Type> ConflictExceptionTypes = new List<Type>
    {
        typeof(AccountWithTheSameEmailExistsException),
        typeof(LocationWithSuchCoordinatesAlreadyExistsException),
    };

    [NonAction]
    protected IActionResult GetResponseFromResult<TValue>(Result<TValue> result)
    {
        return result.Match<IActionResult>(res =>
        {
            return Ok(res);
        }, ex =>
        {
            if (BadRequestExceptionTypes.Contains(ex.GetType()))
            {
                return BadRequest();
            }
            
            if (NotFoundExceptionTypes.Contains(ex.GetType()))
            {
                return NotFound();
            }
            
            if (ConflictExceptionTypes.Contains(ex.GetType()))
            {
                return Conflict();
            }

            return StatusCode(500);
        });
    }
}
