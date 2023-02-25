using DreamChip.AnimalTracking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[Route("[controller]")]
public sealed class LocationsController : BaseController
{
    private readonly LocationService _locationService;

    public LocationsController(LocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _locationService.GetByIdAsync(id);

        return GetResponseFromResult(result);
    }
}
