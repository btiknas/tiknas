using System;
using Tiknas.Domain.Entities;

namespace Tiknas.Auditing.App.Entities;

public class AppEntityWithSoftDelete : AggregateRoot<Guid>, IHasDeletionTime
{
    public AppEntityWithSoftDelete(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletionTime { get; set; }
}
