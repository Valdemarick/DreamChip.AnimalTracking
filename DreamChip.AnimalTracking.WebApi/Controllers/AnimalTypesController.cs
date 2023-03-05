using System.Net;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using DreamChip.AnimalTracking.Application.Dto.AnimalType;
using DreamChip.AnimalTracking.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DreamChip.AnimalTracking.WebApi.Controllers;

[Route("animals/types")]
[ServiceFilter(typeof(CheckAuthorizationDataFilter))]
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

    [HttpPost]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimalTypeDto dto)
    {
        var result = await _animalTypeService.CreateAsync(dto);

        return GetResponseFromResult(result, HttpStatusCode.Created);
    }

    [HttpPut("{id:long}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdateAnimalTypeDto dto)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _animalTypeService.UpdateAsync(id, dto);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }
}
