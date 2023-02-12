using AutoMapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Application.Dto.Account;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Services;

public sealed class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<AccountDto> CreateAccountAsync(CreateAccountDto dto)
    {
        var existingAccount = await _accountRepository.GetByEmail(dto.Email);
        if (existingAccount != null)
        {
            return null;
        }

        var account = _mapper.Map<Account>(dto);

        var id = await _accountRepository.CreateAsync(account);
        account.Id = id;

        return _mapper.Map<AccountDto>(account);
    }
}
