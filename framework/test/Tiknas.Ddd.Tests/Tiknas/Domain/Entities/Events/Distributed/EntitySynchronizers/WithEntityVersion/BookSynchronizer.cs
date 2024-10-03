using System;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Repositories;
using Tiknas.ObjectMapping;

namespace Tiknas.Domain.Entities.Events.Distributed.EntitySynchronizers.WithEntityVersion;

public class BookSynchronizer : EntitySynchronizer<Book, Guid, RemoteBookEto>, ITransientDependency
{
    public BookSynchronizer(IObjectMapper objectMapper, IRepository<Book, Guid> repository)
        : base(objectMapper, repository)
    {
    }
}