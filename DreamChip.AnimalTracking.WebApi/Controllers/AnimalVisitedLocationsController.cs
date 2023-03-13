using System.Net;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using DreamChip.AnimalTracking.Application.Dto.AnimalVisitedLocation;
using DreamChip.AnimalTracking.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[Route("animals")]
[ServiceFilter(typeof(CheckAuthorizationDataFilter))]

public class AnimalVisitedLocationsController : BaseController
{
    private readonly IAnimalVisitedLocationService _animalVisitedLocationService;

    public AnimalVisitedLocationsController(IAnimalVisitedLocationService animalVisitedLocationService)
    {
        _animalVisitedLocationService = animalVisitedLocationService;
    }

    [HttpGet("{animalId:long}/locations")]
    public async Task<IActionResult> GetPageAsync([FromRoute] long animalId,
        [FromQuery] AnimalVisitedLocationPageRequestDto dto)
    {
        if (animalId <= 0)
        {
            return BadRequest();
        }

        var result = await _animalVisitedLocationService.GetPageAsync(animalId, dto);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }
}
