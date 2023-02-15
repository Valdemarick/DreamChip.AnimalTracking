using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AccountRepository : BaseRepository, IAccountRepository
{
    private readonly string[] _columns = { "id", "first_name", "last_name", "email", "password" };

    public AccountRepository(IConfiguration configuration) : base(configuration)
    {
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
