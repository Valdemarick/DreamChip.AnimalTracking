using DreamChip.AnimalTracking.Application.Dto.AnimalType;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.AnimalType;

public sealed class CreateAnimalTypeDtoValidator : AbstractValidator<CreateAnimalTypeDto>
{
    public CreateAnimalTypeDtoValidator()
    {
        RuleFor(x => x.Type)
            .Must(AnimalTypeValidatorRules.IsTypeNameValid);
    }
}
