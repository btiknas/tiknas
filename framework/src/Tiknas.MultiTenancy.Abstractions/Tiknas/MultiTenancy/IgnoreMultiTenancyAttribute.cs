using System;

namespace Tiknas.MultiTenancy;

[AttributeUsage(AttributeTargets.All)]
public class IgnoreMultiTenancyAttribute : Attribute
{

}
