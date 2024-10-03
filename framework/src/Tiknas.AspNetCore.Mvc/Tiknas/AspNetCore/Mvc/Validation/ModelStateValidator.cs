using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tiknas.DependencyInjection;
using Tiknas.Validation;

namespace Tiknas.AspNetCore.Mvc.Validation;

public class ModelStateValidator : IModelStateValidator, ITransientDependency
{
    public virtual void Validate(ModelStateDictionary modelState)
    {
        var validationResult = new TiknasValidationResult();

        AddErrors(validationResult, modelState);

        if (validationResult.Errors.Any())
        {
            throw new TiknasValidationException(
                "ModelState is not valid! See ValidationErrors for details.",
                validationResult.Errors
            );
        }
    }

    public virtual void AddErrors(ITiknasValidationResult validationResult, ModelStateDictionary modelState)
    {
        if (modelState.IsValid)
        {
            return;
        }

        foreach (var state in modelState)
        {
            foreach (var error in state.Value.Errors)
            {
                validationResult.Errors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
            }
        }
    }
}
