using DreamChip.AnimalTracking.Application.Dto.Account;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Account;

/// <summary>
/// Validation for CreateAccountDto.
/// </summary>
public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
{
    public CreateAccountDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("null");

        RuleFor(x => x.LastName)
            .Must(Extensions.IsLastNameValid);

        RuleFor(x => x.Email)
            .EmailAddress()
            .Must(Extensions.IsEmailValid);

        RuleFor(x => x.Password)
            .Must(Extensions.IsPasswordValid);
    }
}
