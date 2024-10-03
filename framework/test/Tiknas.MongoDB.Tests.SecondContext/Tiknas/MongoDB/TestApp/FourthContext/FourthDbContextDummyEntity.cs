using System;
using Tiknas.Domain.Entities;

namespace Tiknas.EntityFrameworkCore.TestApp.FourthContext;

public class FourthDbContextDummyEntity : AggregateRoot<Guid>
{
    public string Value { get; set; }
}
