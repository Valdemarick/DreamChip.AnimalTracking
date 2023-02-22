using DreamChip.AnimalTracking.Application.Dto.Account;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Account;

public sealed class UpdateAccountDtoValidator  :AbstractValidator<UpdateAccountDto>
{
    public UpdateAccountDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .Must(Extensions.IsFirstNameValid);

        RuleFor(x => x.LastName)
            .Must(Extensions.IsLastNameValid);

        RuleFor(x => x.Password)
            .Must(Extensions.IsPasswordValid);

        RuleFor(x => x.Email)
            .Must(Extensions.IsEmailValid);
    }
}
