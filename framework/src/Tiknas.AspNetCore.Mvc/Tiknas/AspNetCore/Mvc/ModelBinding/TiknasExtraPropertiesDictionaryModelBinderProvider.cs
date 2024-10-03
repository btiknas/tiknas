using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiknas.Data;

namespace Tiknas.AspNetCore.Mvc.ModelBinding;

public class TiknasExtraPropertiesDictionaryModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType != typeof(ExtraPropertyDictionary))
        {
            return null;
        }

        if (context.Metadata.ContainerType == null ||
            !context.Metadata.ContainerType.IsAssignableTo<IHasExtraProperties>())
        {
            return null;
        }

        var binderType = typeof(DictionaryModelBinder<string, object>);
        var keyBinder = context.CreateBinder(context.MetadataProvider.GetMetadataForType(typeof(string)));
        var valueBinder = new TiknasExtraPropertyModelBinder(context.Metadata.ContainerType);
        var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
        var mvcOptions = context.Services.GetRequiredService<IOptions<MvcOptions>>().Value;

        return (IModelBinder)Activator.CreateInstance(
            binderType,
            keyBinder,
            valueBinder,
            loggerFactory,
            true /* allowValidatingTopLevelNodes */,
            mvcOptions)!;
    }
}
