using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Repositories.Dapper;
using Tiknas.EntityFrameworkCore;
using Tiknas.TestApp.EntityFrameworkCore;

namespace Tiknas.Dapper.Repositories;

public class PersonDapperRepository : DapperRepository<TestAppDbContext>, ITransientDependency
{
    public PersonDapperRepository(IDbContextProvider<TestAppDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<List<string>> GetAllPersonNames()
    {
        return (await (await GetDbConnectionAsync())
                .QueryAsync<string>(
                    "select Name from People",
                    transaction: await GetDbTransactionAsync()
                )
            ).ToList();
    }

    public virtual async Task<int> UpdatePersonNames(string name)
    {
        return await (await GetDbConnectionAsync())
            .ExecuteAsync("update People set Name = @NewName", new { NewName = name },
                await GetDbTransactionAsync());
    }
}
