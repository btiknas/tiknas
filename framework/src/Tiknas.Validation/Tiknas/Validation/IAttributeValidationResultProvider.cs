using System.ComponentModel.DataAnnotations;

namespace Tiknas.Validation;

public interface IAttributeValidationResultProvider
{
    ValidationResult? GetOrDefault(ValidationAttribute validationAttribute, object? validatingObject, ValidationContext validationContext);
}
