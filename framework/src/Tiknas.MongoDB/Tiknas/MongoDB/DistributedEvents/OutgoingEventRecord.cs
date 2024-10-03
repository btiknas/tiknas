﻿using System;
using Tiknas.Auditing;
using Tiknas.Data;
using Tiknas.Domain.Entities;
using Tiknas.EventBus.Distributed;

namespace Tiknas.MongoDB.DistributedEvents;

public class OutgoingEventRecord :
    BasicAggregateRoot<Guid>,
    IHasExtraProperties,
    IHasCreationTime
{
    public static int MaxEventNameLength { get; set; } = 256;

    public ExtraPropertyDictionary ExtraProperties { get; private set; }

    public string EventName { get; private set; } = default!;

    public byte[] EventData { get; private set; } = default!;

    public DateTime CreationTime { get; private set; }

    protected OutgoingEventRecord()
    {
        ExtraProperties = new ExtraPropertyDictionary();
        this.SetDefaultsForExtraProperties();
    }

    public OutgoingEventRecord(
        OutgoingEventInfo eventInfo)
        : base(eventInfo.Id)
    {
        EventName = eventInfo.EventName;
        EventData = eventInfo.EventData;
        CreationTime = eventInfo.CreationTime;

        ExtraProperties = new ExtraPropertyDictionary();
        this.SetDefaultsForExtraProperties();
        foreach (var property in eventInfo.ExtraProperties)
        {
            this.SetProperty(property.Key, property.Value);
        }
    }

    public OutgoingEventInfo ToOutgoingEventInfo()
    {
        var info = new OutgoingEventInfo(
            Id,
            EventName,
            EventData,
            CreationTime
        );

        foreach (var property in ExtraProperties)
        {
            info.SetProperty(property.Key, property.Value);
        }

        return info;
    }
}
