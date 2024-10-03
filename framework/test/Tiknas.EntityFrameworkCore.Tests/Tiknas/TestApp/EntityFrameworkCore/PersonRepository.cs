using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiknas.Domain.Repositories.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore;
using Tiknas.TestApp.Domain;

namespace Tiknas.TestApp.EntityFrameworkCore;

public class PersonRepository : EfCoreRepository<TestAppDbContext, Person, Guid>, IPersonRepository
{
    public PersonRepository(IDbContextProvider<TestAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<PersonView> GetViewAsync(string name)
    {
        return await (await GetDbContextAsync()).PersonView.Where(x => x.Name == name).FirstOrDefaultAsync();
    }
}
