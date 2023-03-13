﻿using DreamChip.AnimalTracking.Application.Dto.Animal;
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
            .GreaterThan(0).When(x => x.ChipperId is not null);

        RuleFor(x => x.ChippingLocationId)
            .GreaterThan(0).When(x => x.ChippingLocationId is not null);

        RuleFor(x => x.Gender)
            .Must(AnimalValidatorExtensions.IsGenderValid).When(x => x.Gender is not null);

        RuleFor(x => x.LifeStatus)
            .Must(AnimalValidatorExtensions.IsLifeStatusValid).When(x => x.LifeStatus is not null);

        RuleFor(x => x.StartDateTime)
            .Must(AnimalValidatorExtensions.IsDateTimeValidFormat).When(x => x.StartDateTime is not null);

        RuleFor(x => x.EndDateTime)
            .Must(AnimalValidatorExtensions.IsDateTimeValidFormat).When(x => x.EndDateTime is not null);
    }
}
