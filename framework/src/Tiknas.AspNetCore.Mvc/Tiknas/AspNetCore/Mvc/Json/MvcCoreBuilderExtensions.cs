using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Json.SystemTextJson;
using Tiknas.Json.SystemTextJson.JsonConverters;

namespace Tiknas.AspNetCore.Mvc.Json;

public static class MvcCoreBuilderExtensions
{
    public static IMvcCoreBuilder AddTiknasJson(this IMvcCoreBuilder builder)
    {
        builder.Services.AddOptions<JsonOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                options.JsonSerializerOptions.AllowTrailingCommas = true;

                options.JsonSerializerOptions.Converters.Add(new TiknasStringToEnumFactory());
                options.JsonSerializerOptions.Converters.Add(new TiknasStringToBooleanConverter());
                options.JsonSerializerOptions.Converters.Add(new TiknasStringToGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new TiknasNullableStringToGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());

                options.JsonSerializerOptions.TypeInfoResolver = new TiknasDefaultJsonTypeInfoResolver(rootServiceProvider
                    .GetRequiredService<IOptions<TiknasSystemTextJsonSerializerModifiersOptions>>());
            });

        return builder;
    }
}
