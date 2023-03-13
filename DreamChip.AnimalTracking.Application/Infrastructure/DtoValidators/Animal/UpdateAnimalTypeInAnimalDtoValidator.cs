using DreamChip.AnimalTracking.Application.Dto.Animal;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Animal;

public sealed class UpdateAnimalTypeInAnimalDtoValidator : AbstractValidator<UpdateAnimalTypeInAnimalDto>
{
    public UpdateAnimalTypeInAnimalDtoValidator()
    {
        RuleFor(x => x.OldAnimalTypeId)
            .GreaterThan(0);

        RuleFor(x => x.NewAnimalTypeId)
            .GreaterThan(0);
    }
}
