﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.Validation;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.ModelBinding.Metadata;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IModelMetadataProvider))]
public class TiknasModelMetadataProvider : DefaultModelMetadataProvider
{
    public TiknasModelMetadataProvider(ICompositeMetadataDetailsProvider detailsProvider)
        : base(detailsProvider)
    {
    }

    public TiknasModelMetadataProvider(ICompositeMetadataDetailsProvider detailsProvider, IOptions<MvcOptions> optionsAccessor)
        : base(detailsProvider, optionsAccessor)
    {
    }

    protected override DefaultMetadataDetails[] CreatePropertyDetails(ModelMetadataIdentity key)
    {
        var details = base.CreatePropertyDetails(key);

        foreach (var detail in details)
        {
            NormalizeMetadataDetail(detail);
        }

        return details;
    }

    private void NormalizeMetadataDetail(DefaultMetadataDetails detail)
    {
        foreach (var validationAttribute in detail.ModelAttributes.Attributes.OfType<ValidationAttribute>())
        {
            NormalizeValidationAttribute(validationAttribute);
        }
    }

    protected virtual void NormalizeValidationAttribute(ValidationAttribute validationAttribute)
    {
        if (validationAttribute.ErrorMessage == null)
        {
            ValidationAttributeHelper.SetDefaultErrorMessage(validationAttribute);
        }
    }
}
