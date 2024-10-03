using System;
using System.Text.Encodings.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Data;
using Tiknas.Json.SystemTextJson.JsonConverters;
using Tiknas.Json.SystemTextJson.Modifiers;
using Tiknas.Modularity;
using Tiknas.Timing;

namespace Tiknas.Json.SystemTextJson;

[DependsOn(typeof(TiknasJsonAbstractionsModule), typeof(TiknasTimingModule), typeof(TiknasDataModule))]
public class TiknasJsonSystemTextJsonModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddOptions<TiknasSystemTextJsonSerializerOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                // If the user hasn't explicitly configured the encoder, use the less strict encoder that does not encode all non-ASCII characters.
                options.JsonSerializerOptions.Encoder ??= JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

                options.JsonSerializerOptions.Converters.Add(new TiknasStringToEnumFactory());
                options.JsonSerializerOptions.Converters.Add(new TiknasStringToBooleanConverter());
                options.JsonSerializerOptions.Converters.Add(new TiknasStringToGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new TiknasNullableStringToGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());

                options.JsonSerializerOptions.TypeInfoResolver = new TiknasDefaultJsonTypeInfoResolver(rootServiceProvider
                    .GetRequiredService<IOptions<TiknasSystemTextJsonSerializerModifiersOptions>>());
            });

        context.Services.AddOptions<TiknasSystemTextJsonSerializerModifiersOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.Modifiers.Add(new TiknasDateTimeConverterModifier(
                    rootServiceProvider.GetRequiredService<TiknasDateTimeConverter>(),
                    rootServiceProvider.GetRequiredService<TiknasNullableDateTimeConverter>()).CreateModifyAction());
            });
    }
}
