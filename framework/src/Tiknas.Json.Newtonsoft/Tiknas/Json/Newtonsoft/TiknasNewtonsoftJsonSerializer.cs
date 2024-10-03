using System;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Tiknas.DependencyInjection;

namespace Tiknas.Json.Newtonsoft;

[Dependency(ReplaceServices = true)]
public class TiknasNewtonsoftJsonSerializer : IJsonSerializer, ITransientDependency
{
    protected IRootServiceProvider RootServiceProvider { get; }
    protected IOptions<TiknasNewtonsoftJsonSerializerOptions> Options { get; }

    public TiknasNewtonsoftJsonSerializer(IRootServiceProvider rootServiceProvider, IOptions<TiknasNewtonsoftJsonSerializerOptions> options)
    {
        RootServiceProvider = rootServiceProvider;
        Options = options;
    }

    public string Serialize(object obj, bool camelCase = true, bool indented = false)
    {
        return JsonConvert.SerializeObject(obj, CreateJsonSerializerOptions(camelCase, indented));
    }

    public T Deserialize<T>(string jsonString, bool camelCase = true)
    {
        return JsonConvert.DeserializeObject<T>(jsonString, CreateJsonSerializerOptions(camelCase))!;
    }

    public object Deserialize(Type type, string jsonString, bool camelCase = true)
    {
        return JsonConvert.DeserializeObject(jsonString, type, CreateJsonSerializerOptions(camelCase))!;
    }

    private readonly static ConcurrentDictionary<object, JsonSerializerSettings> JsonSerializerOptionsCache =
        new ConcurrentDictionary<object, JsonSerializerSettings>();

    protected virtual JsonSerializerSettings CreateJsonSerializerOptions(bool camelCase = true, bool indented = false)
    {
        return JsonSerializerOptionsCache.GetOrAdd(new
        {
            camelCase,
            indented
        }, _ =>
        {
            var settings = new JsonSerializerSettings
            {
                SerializationBinder = Options.Value.JsonSerializerSettings.SerializationBinder,
                CheckAdditionalContent = Options.Value.JsonSerializerSettings.CheckAdditionalContent,
                Context = Options.Value.JsonSerializerSettings.Context,
                ContractResolver = Options.Value.JsonSerializerSettings.ContractResolver,
                ConstructorHandling = Options.Value.JsonSerializerSettings.ConstructorHandling,
                Converters = Options.Value.JsonSerializerSettings.Converters,
                Culture = Options.Value.JsonSerializerSettings.Culture,
                DateFormatHandling = Options.Value.JsonSerializerSettings.DateFormatHandling,
                DateFormatString = Options.Value.JsonSerializerSettings.DateFormatString,
                DateParseHandling = Options.Value.JsonSerializerSettings.DateParseHandling,
                DateTimeZoneHandling = Options.Value.JsonSerializerSettings.DateTimeZoneHandling,
                DefaultValueHandling = Options.Value.JsonSerializerSettings.DefaultValueHandling,
                Error = Options.Value.JsonSerializerSettings.Error,
                EqualityComparer = Options.Value.JsonSerializerSettings.EqualityComparer,
                FloatFormatHandling = Options.Value.JsonSerializerSettings.FloatFormatHandling,
                FloatParseHandling = Options.Value.JsonSerializerSettings.FloatParseHandling,
                Formatting = Options.Value.JsonSerializerSettings.Formatting,
                MaxDepth = Options.Value.JsonSerializerSettings.MaxDepth,
                MetadataPropertyHandling = Options.Value.JsonSerializerSettings.MetadataPropertyHandling,
                MissingMemberHandling = Options.Value.JsonSerializerSettings.MissingMemberHandling,
                NullValueHandling = Options.Value.JsonSerializerSettings.NullValueHandling,
                ObjectCreationHandling = Options.Value.JsonSerializerSettings.ObjectCreationHandling,
                PreserveReferencesHandling = Options.Value.JsonSerializerSettings.PreserveReferencesHandling,
                ReferenceLoopHandling = Options.Value.JsonSerializerSettings.ReferenceLoopHandling,
                ReferenceResolverProvider = Options.Value.JsonSerializerSettings.ReferenceResolverProvider,
                StringEscapeHandling = Options.Value.JsonSerializerSettings.StringEscapeHandling,
                TraceWriter = Options.Value.JsonSerializerSettings.TraceWriter,
                TypeNameHandling = Options.Value.JsonSerializerSettings.TypeNameHandling,
                TypeNameAssemblyFormatHandling = Options.Value.JsonSerializerSettings.TypeNameAssemblyFormatHandling
            };

            if (!camelCase)
            {
                //Default contract resolver is TiknasCamelCasePropertyNamesContractResolver}
                settings.ContractResolver = new TiknasDefaultContractResolver(RootServiceProvider.GetRequiredService<TiknasDateTimeConverter>());
            }

            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }

            return settings;
        });
    }
}
