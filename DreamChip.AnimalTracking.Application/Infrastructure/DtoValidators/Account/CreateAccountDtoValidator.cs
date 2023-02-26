using DreamChip.AnimalTracking.Application.Dto.Account;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Account;

/// <summary>
/// Validation for CreateAccountDto.
/// </summary>
public sealed class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
{
    public CreateAccountDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .Must(AccountValidatorRules.IsFirstNameValid);

        RuleFor(x => x.LastName)
            .Must(AccountValidatorRules.IsLastNameValid);

        RuleFor(x => x.Email)
            .EmailAddress()
            .Must(AccountValidatorRules.IsEmailValid);

        RuleFor(x => x.Password)
            .Must(AccountValidatorRules.IsPasswordValid);
    }
}
