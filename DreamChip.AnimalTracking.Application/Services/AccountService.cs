using AutoMapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Application.Dto.Account;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.Exceptions;
using DreamChip.AnimalTracking.Domain.Exceptions.Account;
using DreamChip.AnimalTracking.Domain.ValueObjects.Account;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Services;

public sealed class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public AccountService(
        IAccountRepository accountRepository,
        IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<Result<AccountDto>> GetByIdAsync(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            return new Result<AccountDto>(new AccountNotFoundException());
        }

        var accountDto = _mapper.Map<AccountDto>(account);

        return new Result<AccountDto>(accountDto);
    }

    public async Task<Result<List<AccountDto>>> GetPageAsync(AccountPageRequestDto dto)
    {
        var request = _mapper.Map<AccountPageRequest>(dto);

        var accounts = await _accountRepository.GetPageAsync(request);

        var accountsDto = _mapper.Map<List<AccountDto>>(accounts);

        return accountsDto;
    }

    public async Task<Result<AccountDto>> CreateAsync(CreateAccountDto dto)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(dto.Email);
        if (existingAccount != null)
        {
            var exception = new AccountWithTheSameEmailExistsException();
        
            return new Result<AccountDto>(exception);
        }

        var account = _mapper.Map<Account>(dto);

        var id = await _accountRepository.CreateAsync(account);
        account.Id = id;

        var accountDto = _mapper.Map<AccountDto>(account);

        return new Result<AccountDto>(accountDto);
    }

    public async Task<Result<AccountDto>> UpdateAsync(int id, UpdateAccountDto dto)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            return new Result<AccountDto>(new AccountNotFoundException());
        }

        account = await _accountRepository.GetByEmailAsync(dto.Email);
        if (account != null && account.Id != id)
        {
            return new Result<AccountDto>(new AccountWithTheSameEmailExistsException());
        }

        account = _mapper.Map<Account>(dto);
        account.Id = id;
        
        await _accountRepository.UpdateAsync(account);

        var accountDto = _mapper.Map<AccountDto>(account);

        return new Result<AccountDto>(accountDto);
    }

    public async Task<Result<AccountDto>> DeleteAsync(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            return new Result<AccountDto>(new AccountNotFoundException());
        }

        if (account.Animals.Count > 0)
        {
            return new Result<AccountDto>(new AccountLinkedWithAnimalsException());
        }

        await _accountRepository.DeleteAsync(id);

        return new Result<AccountDto>(new AccountDto());
    }
}
