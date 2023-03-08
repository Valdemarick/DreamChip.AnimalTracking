using DreamChip.AnimalTracking.Application.Dto.Animal;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Animal;

public sealed class AnimalPageRequestDtoValidator : AbstractValidator<AnimalPageRequestDto>
{
    public AnimalPageRequestDtoValidator()
    {
        RuleFor(x => x.From)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Size)
            .GreaterThan(0);

        RuleFor(x => x.ChipperId)
            .GreaterThan(0);

        RuleFor(x => x.ChippingLocationId)
            .GreaterThan(0);

        RuleFor(x => x.Gender)
            .NotNull()
            .Must(AnimalValidatorExtensions.IsGenderValid);

        RuleFor(x => x.LifeStatus)
            .NotNull()
            .Must(AnimalValidatorExtensions.IsLifeStatusValid);
    }
}
