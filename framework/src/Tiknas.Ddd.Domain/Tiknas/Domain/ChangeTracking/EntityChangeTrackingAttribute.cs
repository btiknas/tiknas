using System;

namespace Tiknas.Domain.ChangeTracking;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public abstract class EntityChangeTrackingAttribute : Attribute
{
    public virtual bool IsEnabled { get; set; }

    public EntityChangeTrackingAttribute(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }
}
