﻿using System;
using Tiknas.Domain.Entities.Auditing;

namespace Tiknas.TestApp.Domain;

public class District : CreationAuditedEntity
{
    public Guid CityId { get; private set; }

    public string Name { get; private set; }

    public int Population { get; set; }

    private District()
    {

    }

    public District(Guid cityId, string name, int population = 0)
    {
        CityId = cityId;
        Name = name;
    }

    public override object[] GetKeys()
    {
        return new Object[] { CityId, Name };
    }
}
