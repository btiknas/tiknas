using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tiknas.Validation;

namespace Tiknas.AspNetCore.Mvc.Validation;

public interface IModelStateValidator
{
    void Validate(ModelStateDictionary modelState);

    void AddErrors(ITiknasValidationResult validationResult, ModelStateDictionary modelState);
}
