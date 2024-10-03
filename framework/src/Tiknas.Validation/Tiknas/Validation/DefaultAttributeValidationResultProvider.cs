using System.ComponentModel.DataAnnotations;
using Tiknas.DependencyInjection;

namespace Tiknas.Validation;

public class DefaultAttributeValidationResultProvider : IAttributeValidationResultProvider, ITransientDependency
{
    public virtual ValidationResult? GetOrDefault(ValidationAttribute validationAttribute, object? validatingObject, ValidationContext validationContext)
    {
        return validationAttribute.GetValidationResult(validatingObject, validationContext);
    }
}
