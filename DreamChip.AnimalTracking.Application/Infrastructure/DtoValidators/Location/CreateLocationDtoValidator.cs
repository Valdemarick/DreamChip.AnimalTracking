using DreamChip.AnimalTracking.Application.Dto.Location;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Location;

public class CreateLocationDtoValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationDtoValidator()
    {
        RuleFor(x => x.Latitude)
            .NotNull()
            .GreaterThan(-90.0)
            .LessThan(90.0);

        RuleFor(x => x.Longitude)
            .NotNull()
            .GreaterThan(-180.0)
            .LessThan(180.0);
    }    
}
