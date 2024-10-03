using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Mvc.Auditing;
using Tiknas.AspNetCore.Mvc.ContentFormatters;
using Tiknas.AspNetCore.Mvc.Conventions;
using Tiknas.AspNetCore.Mvc.ExceptionHandling;
using Tiknas.AspNetCore.Mvc.Features;
using Tiknas.AspNetCore.Mvc.GlobalFeatures;
using Tiknas.AspNetCore.Mvc.ModelBinding;
using Tiknas.AspNetCore.Mvc.Response;
using Tiknas.AspNetCore.Mvc.Uow;
using Tiknas.AspNetCore.Mvc.Validation;
using Tiknas.Content;

namespace Tiknas.AspNetCore.Mvc;

internal static class TiknasMvcOptionsExtensions
{
    public static void AddTiknas(this MvcOptions options, IServiceCollection services)
    {
        AddConventions(options, services);
        AddActionFilters(options);
        AddPageFilters(options);
        AddModelBinders(options);
        AddMetadataProviders(options, services);
        AddFormatters(options);
    }

    private static void AddFormatters(MvcOptions options)
    {
        options.OutputFormatters.Insert(0, new RemoteStreamContentOutputFormatter());
    }

    private static void AddConventions(MvcOptions options, IServiceCollection services)
    {
        options.Conventions.Add(new TiknasServiceConventionWrapper(services));
    }

    private static void AddActionFilters(MvcOptions options)
    {
        options.Filters.AddService(typeof(GlobalFeatureActionFilter));
        options.Filters.AddService(typeof(TiknasAuditActionFilter));
        options.Filters.AddService(typeof(TiknasNoContentActionFilter));
        options.Filters.AddService(typeof(TiknasFeatureActionFilter));
        options.Filters.AddService(typeof(TiknasValidationActionFilter));
        options.Filters.AddService(typeof(TiknasUowActionFilter));
        options.Filters.AddService(typeof(TiknasExceptionFilter));
    }

    private static void AddPageFilters(MvcOptions options)
    {
        options.Filters.AddService(typeof(GlobalFeaturePageFilter));
        options.Filters.AddService(typeof(TiknasExceptionPageFilter));
        options.Filters.AddService(typeof(TiknasAuditPageFilter));
        options.Filters.AddService(typeof(TiknasFeaturePageFilter));
        options.Filters.AddService(typeof(TiknasUowPageFilter));
    }

    private static void AddModelBinders(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new TiknasDateTimeModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new TiknasExtraPropertiesDictionaryModelBinderProvider());
        options.ModelBinderProviders.Insert(2, new TiknasRemoteStreamContentModelBinderProvider());
    }

    private static void AddMetadataProviders(MvcOptions options, IServiceCollection services)
    {
        options.ModelMetadataDetailsProviders.Add(new TiknasDataAnnotationAutoLocalizationMetadataDetailsProvider(services));

        options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IRemoteStreamContent), BindingSource.FormFile));
        options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IEnumerable<IRemoteStreamContent>), BindingSource.FormFile));
        options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(RemoteStreamContent), BindingSource.FormFile));
        options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IEnumerable<RemoteStreamContent>), BindingSource.FormFile));
        options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(IRemoteStreamContent)));
        options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(RemoteStreamContent)));
    }
}
