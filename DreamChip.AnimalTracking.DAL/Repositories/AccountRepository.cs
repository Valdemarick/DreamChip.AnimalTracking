﻿using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Account;
using LanguageExt;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AccountRepository : BaseRepository, IAccountRepository
{
    private readonly string[] _columns = { "id", "first_name", "last_name", "email", "password" };

    public AccountRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Account?> GetByIdAsync(long id)
    {
        var sql = $@"SELECT {string.Join(',', _columns)}
                     FROM public.account
                     WHERE id = @id";

        var connection = await OpenConnection();

        var account = await connection.QueryFirstOrDefaultAsync<Account>(sql, new { id });

        return account;
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        var sql = @$"SELECT {string.Join(',', _columns)}
                    FROM public.account
                    WHERE email = @email";

        var connection = await OpenConnection();

        var account = await connection.QueryFirstOrDefaultAsync<Account>(sql, new { email });
        
        return account;
    }

    public async Task<List<Account>> GetPageAsync(AccountPageRequest request)
    {
        var sql = $@"SELECT {string.Join(',', _columns)}
                     FROM public.account";

        var filter = CreateAccountPageFilter(request);

        sql += $@"{filter}
                   ORDER BY id
                   OFFSET @from ROWS FETCH NEXT @size ROWS ONLY";

        var connection = await OpenConnection();

        var accounts = await connection.QueryAsync<Account>(sql, new
        {
            firstName = $"%{request.FirstName}$",
            lastName = request.LastName?.ToLower(),
            email = $"%{request.Email}%",
            from = request.From,
            size = request.Size
        });

        return accounts.ToList();
    }

    public async Task<int> CreateAsync(Account account)
    {
        var sql = @$"INSERT INTO public.account (first_name, last_name, email, password)
                    VALUES (@FirstName, @LastName, @Email, @Password)
                    RETURNING id";

        var connection = await OpenConnection();
        using var transaction = connection.BeginTransaction();
        await using var command = new NpgsqlCommand(sql);

        var id = await connection.ExecuteScalarAsync<int>(command.CommandText, account);

        transaction.Commit();

        return id;
    }

    private string? CreateAccountPageFilter(AccountPageRequest request)
    {
        var filterConditions = new List<string>();

        if (request.LastName != null)
        {
            filterConditions.Add("lower(last_name) LIKE @lastName");
        }

        if (request.FirstName != null)
        {
            filterConditions.Add("first_name LIKE @firstName");
        }

        if (request.Email != null)
        {
            filterConditions.Add("email LIKE @email");
        }

        var filter = filterConditions.Any() ? $" WHERE ({string.Join(") AND (", filterConditions)})" : null;

        return filter;
    }
}
