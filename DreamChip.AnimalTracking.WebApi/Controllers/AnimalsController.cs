using System.Net;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using DreamChip.AnimalTracking.Application.Dto.Animal;
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
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _animalService.GetByIdAsync(id);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetPageAsync([FromQuery] AnimalPageRequestDto dto)
    {
        var result = await _animalService.GetPageAsync(dto);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAnimalDto dto)
    {
        var result = await _animalService.CreateAsync(dto);

        return GetResponseFromResult(result, HttpStatusCode.Created);
    }

    [HttpPost("{animalId:long}/types/{typeId:long}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> AddTypeAsync([FromRoute] long animalId, [FromRoute] long typeId)
    {
        if (animalId <= 0 || typeId <= 0)
        {
            return BadRequest();
        }

        var result = await _animalService.AddTypeAsync(animalId, typeId);

        return GetResponseFromResult(result, HttpStatusCode.Created);
    }

    [HttpPut("{animalId:long}/types")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> UpdateAnimalTypeAsync([FromRoute] long animalId, [FromBody] UpdateAnimalTypeInAnimalDto dto)
    {
        if (animalId <= 0)
        {
            return BadRequest();
        }

        var result = await _animalService.UpdateAnimalTypeInAnimalAsync(animalId, dto);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }

    [HttpDelete("{id:long}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _animalService.DeleteAsync(id);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }

    [HttpDelete("{animalId:long}/types/{animalTypeId:long}")]
    public async Task<IActionResult> DeleteAnimalTypeFromAnimalAsync([FromRoute] long animalId,
        [FromRoute] long animalTypeId)
    {
        if (animalId <= 0 || animalTypeId <= 0)
        {
            return BadRequest();
        }

        var result = await _animalService.DeleteAnimalTypeFromAnimalAsync(animalId, animalTypeId);

        return GetResponseFromResult(result, HttpStatusCode.OK);
    }
}
