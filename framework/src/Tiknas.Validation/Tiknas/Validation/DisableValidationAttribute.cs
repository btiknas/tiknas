using System;

namespace Tiknas.Validation;

/// <summary>
/// Can be added to a method to disable auto validation.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
public class DisableValidationAttribute : Attribute
{

}
