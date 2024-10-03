using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tiknas.Domain.Entities;

namespace Tiknas.MongoDB.TestApp.SecondContext;

public class PhoneInSecondDbContext : AggregateRoot
{
    public virtual Guid PersonId { get; set; }

    public virtual string Number { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { PersonId, Number };
    }
}
