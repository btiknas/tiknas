using System;
using Tiknas.Domain.Entities;

namespace Tiknas.TestApp.Domain;

public class Category : AggregateRoot<Guid>, ISoftDelete
{
    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}
