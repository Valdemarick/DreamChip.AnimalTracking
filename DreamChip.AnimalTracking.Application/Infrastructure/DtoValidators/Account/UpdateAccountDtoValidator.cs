using DreamChip.AnimalTracking.Application.Dto.Account;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Account;

public sealed class UpdateAccountDtoValidator  :AbstractValidator<UpdateAccountDto>
{
    public UpdateAccountDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .Must(AccountValidatorRules.IsFirstNameValid);

        RuleFor(x => x.LastName)
            .Must(AccountValidatorRules.IsLastNameValid);

        RuleFor(x => x.Password)
            .Must(AccountValidatorRules.IsPasswordValid);

        RuleFor(x => x.Email)
            .Must(AccountValidatorRules.IsEmailValid);
    }
}
