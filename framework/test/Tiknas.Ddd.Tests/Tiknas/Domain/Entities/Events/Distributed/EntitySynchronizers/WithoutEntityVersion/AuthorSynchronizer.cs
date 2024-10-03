using System;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Repositories;
using Tiknas.ObjectMapping;

namespace Tiknas.Domain.Entities.Events.Distributed.EntitySynchronizers.WithoutEntityVersion;

public class AuthorSynchronizer : EntitySynchronizer<Author, Guid, RemoteAuthorEto>, ITransientDependency
{
    public AuthorSynchronizer(IObjectMapper objectMapper, IRepository<Author, Guid> repository)
        : base(objectMapper, repository)
    {
    }
}