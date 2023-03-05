using DreamChip.AnimalTracking.Application.Dto.AnimalType;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.AnimalType;

public sealed class UpdateAnimalTypeDtoValidator : AbstractValidator<UpdateAnimalTypeDto>
{
    public UpdateAnimalTypeDtoValidator()
    {
        RuleFor(x => x.Type)
            .Must(AnimalTypeValidatorRules.IsTypeNameValid);
    }    
}
