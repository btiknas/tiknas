using System;
using Tiknas.Domain.Entities;

namespace Tiknas.EntityFrameworkCore.TestApp.ThirdDbContext;

public class ThirdDbContextDummyEntity : AggregateRoot<Guid>
{
    public string Value { get; set; }
}
