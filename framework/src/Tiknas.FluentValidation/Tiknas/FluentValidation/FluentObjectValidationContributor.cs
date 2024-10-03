using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Validation;

namespace Tiknas.FluentValidation;

public class FluentObjectValidationContributor : IObjectValidationContributor, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public FluentObjectValidationContributor(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public virtual async Task AddErrorsAsync(ObjectValidationContext context)
    {
        var serviceType = typeof(IValidator<>).MakeGenericType(context.ValidatingObject.GetType());
        var validator = _serviceProvider.GetService(serviceType) as IValidator;
        if (validator == null)
        {
            return;
        }

        var result = await validator.ValidateAsync((IValidationContext)Activator.CreateInstance(
            typeof(ValidationContext<>).MakeGenericType(context.ValidatingObject.GetType()),
            context.ValidatingObject)!);

        if (!result.IsValid)
        {
            context.Errors.AddRange(
                result.Errors.Select(
                    error =>
                        new ValidationResult(error.ErrorMessage, new[] { error.PropertyName })
                )
            );
        }
    }
}
