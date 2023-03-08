using System.Net;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using DreamChip.AnimalTracking.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[Route("[controller]")]
[ServiceFilter(typeof(CheckAuthorizationDataFilter))]
public sealed class AnimalsController : BaseController
{
    private readonly IAnimalService _animalService;

    public AnimalsController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult>? GetByIdAsync([FromRoute] long id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _animalService.GetByIdAsync(id);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }
}
