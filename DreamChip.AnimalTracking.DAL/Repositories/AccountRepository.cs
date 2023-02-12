using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AccountRepository : BaseRepository, IAccountRepository
{
    public AccountRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Account?> GetByEmail(string email)
    {
        var sql = @"SELECT id, first_name, last_name, email, password
                    FROM public.account
                    WHERE email = @Email";

        var connection = await OpenConnection();

        var account = await connection.QueryFirstAsync<Account>(sql, email);

        return account;
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
}
