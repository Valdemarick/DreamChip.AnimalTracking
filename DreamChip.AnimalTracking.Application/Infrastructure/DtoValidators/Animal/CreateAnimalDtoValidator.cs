using DreamChip.AnimalTracking.Application.Dto.Animal;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Animal;

public sealed class CreateAnimalDtoValidator : AbstractValidator<CreateAnimalDto>
{
    public CreateAnimalDtoValidator()
    {
        RuleFor(x => x.AnimalTypes)
            .NotEmpty();

        RuleFor(x => x.Weight)
            .GreaterThan(0);

        RuleFor(x => x.Length)
            .GreaterThan(0);

        RuleFor(x => x.Height)
            .GreaterThan(0);

        RuleFor(x => x.Gender)
            .NotNull()
            .Must(AnimalValidatorExtensions.IsGenderValid);

        RuleFor(x => x.ChipperId)
            .GreaterThan(0);

        RuleFor(x => x.ChippingLocationId)
            .GreaterThan(0);
    }    
}
