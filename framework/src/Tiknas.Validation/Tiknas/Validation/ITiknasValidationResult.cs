using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiknas.Validation;

public interface ITiknasValidationResult
{
    List<ValidationResult> Errors { get; }
}
