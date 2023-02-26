using System.Net;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[Route("animals/types")]
public sealed class AnimalTypesController : BaseController
{
    private readonly IAnimalTypeService _animalTypeService;

    public AnimalTypesController(IAnimalTypeService animalTypeService)
    {
        _animalTypeService = animalTypeService;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _animalTypeService.GetByIdAsync(id);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }
}