using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiknas.Validation;

public class TiknasValidationResult : ITiknasValidationResult
{
    public List<ValidationResult> Errors { get; }

    public TiknasValidationResult()
    {
        Errors = new List<ValidationResult>();
    }
}
