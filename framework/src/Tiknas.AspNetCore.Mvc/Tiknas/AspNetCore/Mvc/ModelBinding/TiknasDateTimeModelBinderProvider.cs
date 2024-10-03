using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tiknas.Timing;

namespace Tiknas.AspNetCore.Mvc.ModelBinding;

public class TiknasDateTimeModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var modelType = context.Metadata.UnderlyingOrModelType;
        if (modelType == typeof(DateTime))
        {
            if (context.Metadata.ContainerType == null)
            {
                if (context.Metadata is DefaultModelMetadata defaultModelMetadata &&
                    defaultModelMetadata.Attributes.Attributes.All(x => x.GetType() != typeof(DisableDateTimeNormalizationAttribute)))
                {
                    return CreateTiknasDateTimeModelBinder(context);
                }
            }
            else
            {
                var dateNormalizationDisabledForClass =
                    context.Metadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

                var dateNormalizationDisabledForProperty = context.Metadata.ContainerType
                    .GetProperty(context.Metadata.PropertyName!)
                    ?.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

                if (!dateNormalizationDisabledForClass &&
                    dateNormalizationDisabledForProperty != null &&
                    !dateNormalizationDisabledForProperty.Value)
                {
                    return CreateTiknasDateTimeModelBinder(context);
                }
            }
        }

        return null;
    }

    protected virtual TiknasDateTimeModelBinder CreateTiknasDateTimeModelBinder(ModelBinderProviderContext context)
    {
        const DateTimeStyles supportedStyles = DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal;
        var dateTimeModelBinder = new DateTimeModelBinder(supportedStyles, context.Services.GetRequiredService<ILoggerFactory>());
        return new TiknasDateTimeModelBinder(context.Services.GetRequiredService<IClock>(), dateTimeModelBinder);
    }
}
