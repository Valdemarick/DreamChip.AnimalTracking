using DreamChip.AnimalTracking.Application.Dto.Account;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Account;

public sealed class AccountPageRequestDtoValidator : AbstractValidator<AccountPageRequestDto>
{
    public AccountPageRequestDtoValidator()
    {
        RuleFor(x => x.From)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Size)
            .GreaterThan(0);
    }
}
