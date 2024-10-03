using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiknas.Validation;

public interface IHasValidationErrors
{
    IList<ValidationResult> ValidationErrors { get; }
}
