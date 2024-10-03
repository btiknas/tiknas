using System;

namespace Tiknas.DependencyInjection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class DisablePropertyInjectionAttribute : Attribute
{

}
