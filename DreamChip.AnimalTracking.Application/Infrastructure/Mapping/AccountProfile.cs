using AutoMapper;
using DreamChip.AnimalTracking.Application.Dto.Account;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Account;

namespace DreamChip.AnimalTracking.Application.Infrastructure.Mapping;

internal sealed class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<CreateAccountDto, Account>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Animals, opt => opt.Ignore());

        CreateMap<Account, AccountDto>();

        CreateMap<AccountPageRequestDto, AccountPageRequest>();

        CreateMap<UpdateAccountDto, Account>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
