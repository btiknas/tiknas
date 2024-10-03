using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.Validation;
using Tiknas.DependencyInjection;
using Tiknas.Validation;

namespace Tiknas.AspNetCore.Mvc.Localization;

[Dependency(ReplaceServices = true)]
public class TiknasMvcAttributeValidationResultProvider : DefaultAttributeValidationResultProvider
{
    private readonly TiknasMvcDataAnnotationsLocalizationOptions _tiknasMvcDataAnnotationsLocalizationOptions;
    private readonly IStringLocalizerFactory _stringLocalizerFactory;

    public TiknasMvcAttributeValidationResultProvider(
        IOptions<TiknasMvcDataAnnotationsLocalizationOptions> tiknasMvcDataAnnotationsLocalizationOptions,
        IStringLocalizerFactory stringLocalizerFactory)
    {
        _tiknasMvcDataAnnotationsLocalizationOptions = tiknasMvcDataAnnotationsLocalizationOptions.Value;
        _stringLocalizerFactory = stringLocalizerFactory;
    }

    public override ValidationResult? GetOrDefault(ValidationAttribute validationAttribute, object? validatingObject, ValidationContext validationContext)
    {
        var resourceSource = _tiknasMvcDataAnnotationsLocalizationOptions.AssemblyResources.GetOrDefault(validationContext.ObjectType.Assembly);
        if (resourceSource == null)
        {
            return base.GetOrDefault(validationAttribute, validatingObject, validationContext);
        }

        if (validationAttribute.ErrorMessage == null)
        {
            ValidationAttributeHelper.SetDefaultErrorMessage(validationAttribute);
        }

        if (validationAttribute.ErrorMessage != null)
        {
            validationAttribute.ErrorMessage = _stringLocalizerFactory.Create(resourceSource)[validationAttribute.ErrorMessage];
        }

        return base.GetOrDefault(validationAttribute, validatingObject, validationContext);
    }
}
