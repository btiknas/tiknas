using System;
using System.Linq;
using Tiknas.MultiTenancy;

namespace Tiknas.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ReplaceDbContextAttribute : Attribute
{
    public MultiTenantDbContextType[] ReplacedDbContextTypes { get; }

    public ReplaceDbContextAttribute(params Type[] replacedDbContextTypes)
    {
        ReplacedDbContextTypes = replacedDbContextTypes.Select(type => new MultiTenantDbContextType(type)).ToArray();
    }

    public ReplaceDbContextAttribute(Type replacedDbContextType, MultiTenancySides side)
    {
        ReplacedDbContextTypes = new[] {new MultiTenantDbContextType(replacedDbContextType, side)};
    }
}
