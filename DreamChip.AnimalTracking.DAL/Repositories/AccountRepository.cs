using System.Text;
using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.DAL.Extensions;
using DreamChip.AnimalTracking.DAL.RepositoryMetadata;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Account;
using Microsoft.Extensions.Configuration;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AccountRepository : BaseRepository<Account, int>, IAccountRepository
{
    public AccountRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string TableName { get; set; } = nameof(Account);
    protected override string[] Columns { get; set; } = { "Id", "FirstName", "LastName", "Email", "Password" };
    
    public override async Task<Account?> GetByIdAsync(int id)
    {
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .AddColumns(AnimalTableMetadata.TableName, AnimalTableMetadata.Columns)
            .From(TableName)
            .LeftJoin(TableName, AnimalTableMetadata.TableName, "Id", "ChipperId")
            .Where(new []{ $"\"{TableName}\".\"Id\" = @Id"})
            .ToString();

        var dict = new Dictionary<int, Account>();
        
        var connection = await OpenConnection();
    
        var account = (await connection.QueryAsync<Account, Animal, Account>(sql,
            (account, animal) =>
            {
                Account entry;

                if (!dict.TryGetValue(id, out entry))
                {
                    entry = account;
                    dict.Add(id ,entry);
                }
                
                if (animal is not null)
                {
                    account.Animals.Add(animal);
                }
    
                return account;
            }, 
        new { id }))
            .AsQueryable()
            .FirstOrDefault();
        
        connection.Close();
    
        return account;
    }

    public async Task<Account?> GetByEmailAsync(string? email)
    {
        var builder = new StringBuilder()
            .Select(TableName, Columns)
            .From(TableName)
            .Where(new[] { "\"Email\" = @Email" });

        var connection = await OpenConnection();

        var account = await connection.QueryFirstOrDefaultAsync<Account>(builder.ToString(), new { email });
        
        connection.Close();

        return account;
    }

    public async Task<List<Account>> GetPageAsync(AccountPageRequest request)
    {
        var filter = CreateAccountPageFilters(request).ToList();

        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .From(TableName)
            .Where(filter)
            .OrderByAscending("\"Id\"")
            .Offset(request.From, request.Size)
            .ToString();
        
        var connection = await OpenConnection();

        var accounts = await connection.QueryAsync<Account>(sql, new
        {
            firstName = $"%{request.FirstName?.Trim().ToLower()}%",
            lastName = $"%{request.LastName?.Trim().ToLower()}%",
            email = $"%{request.Email?.Trim().ToLower()}%",
            from = request.From,
            size = request.Size
        });
        
        connection.Close();

        return accounts.ToList();
    }

    private IEnumerable<string> CreateAccountPageFilters(AccountPageRequest request)
    {
        var filterConditions = new List<string>();

        if (request.LastName != null)
        {
            filterConditions.Add("lower(\"LastName\") LIKE @lastName");
        }

        if (request.FirstName != null)
        {
            filterConditions.Add("\"FirstName\" LIKE @firstName");
        }

        if (request.Email != null)
        {
            filterConditions.Add("\"Email\" LIKE @email");
        }

        return filterConditions;
    }
}
