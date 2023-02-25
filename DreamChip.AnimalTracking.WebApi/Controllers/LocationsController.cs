using DreamChip.AnimalTracking.Application.Dto.Location;
using DreamChip.AnimalTracking.Application.Services;
using DreamChip.AnimalTracking.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[Route("[controller]")]
[ServiceFilter(typeof(CheckAuthorizationDataFilter))]
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

    [HttpPost]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateLocationDto dto)
    {
        var result = await _locationService.CreateAsync(dto);

        return GetResponseFromResult(result);
    }

    [HttpPut("{id:long}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdateLocationDto dto)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _locationService.UpdateAsync(id, dto);

        return GetResponseFromResult(result);
    }
}
