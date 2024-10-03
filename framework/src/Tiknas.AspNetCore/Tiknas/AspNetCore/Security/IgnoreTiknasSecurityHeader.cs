using System;

namespace Tiknas.AspNetCore.Security;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IgnoreTiknasSecurityHeaderAttribute : Attribute
{
    
}
