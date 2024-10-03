using System;
using Tiknas.Domain.Entities;

namespace Tiknas.EntityFrameworkCore.TestApp.FifthContext;

public class FifthDbContextDummyEntity : AggregateRoot<Guid>
{
    public string Value { get; set; }
}
