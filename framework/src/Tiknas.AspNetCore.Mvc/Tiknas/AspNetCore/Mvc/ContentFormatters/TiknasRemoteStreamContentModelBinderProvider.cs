using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tiknas.Content;

namespace Tiknas.AspNetCore.Mvc.ContentFormatters;

public class TiknasRemoteStreamContentModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(RemoteStreamContent) ||
            typeof(IEnumerable<RemoteStreamContent>).IsAssignableFrom(context.Metadata.ModelType))
        {
            return new TiknasRemoteStreamContentModelBinder<RemoteStreamContent>();
        }

        if (context.Metadata.ModelType == typeof(IRemoteStreamContent) ||
            typeof(IEnumerable<IRemoteStreamContent>).IsAssignableFrom(context.Metadata.ModelType))
        {
            return new TiknasRemoteStreamContentModelBinder<IRemoteStreamContent>();
        }

        return null;
    }
}
