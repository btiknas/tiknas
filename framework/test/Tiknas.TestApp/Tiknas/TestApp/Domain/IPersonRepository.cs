using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiknas.Domain.Repositories;

namespace Tiknas.TestApp.Domain;

public interface IPersonRepository : IBasicRepository<Person, Guid>
{
    Task<PersonView> GetViewAsync(string name);
}
