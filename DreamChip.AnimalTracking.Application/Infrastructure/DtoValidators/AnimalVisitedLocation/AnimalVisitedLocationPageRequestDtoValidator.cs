using DreamChip.AnimalTracking.Application.Dto.AnimalVisitedLocation;
using DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Animal;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.AnimalVisitedLocation;

public sealed class AnimalVisitedLocationPageRequestDtoValidator : AbstractValidator<AnimalVisitedLocationPageRequestDto>
{
    public AnimalVisitedLocationPageRequestDtoValidator()
    {
        RuleFor(x => x.From)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Size)
            .GreaterThan(0);

        RuleFor(x => x.StartDateTime)
            .Must(AnimalValidatorExtensions.IsDateTimeValidFormat).When(x => x.StartDateTime is not null);

        RuleFor(x => x.EndDateTime)
            .Must(AnimalValidatorExtensions.IsDateTimeValidFormat).When(x => x.EndDateTime is not null);
    }    
}
