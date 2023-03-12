using DapperExtensions.Mapper;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.Mappers;

public sealed class AccountMapper : ClassMapper<Account>
{
    public AccountMapper()
    {
        Table(nameof(Account));
        Map(x => x.Id).Column("Id");
        Map(x => x.FirstName).Column("FirstName");
        Map(x => x.LastName).Column("LastName");
        Map(x => x.Email).Column("Email");
        Map(x => x.Password).Column("Password");
        Map(x => x.Animals).Ignore();
    }
}
