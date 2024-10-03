using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.Reflection;

namespace Tiknas.ObjectMapping;

public class TiknasObjectMappingModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnExposing(onServiceExposingContext =>
        {
            //Register types for IObjectMapper<TSource, TDestination> if implements
            onServiceExposingContext.ExposedTypes.AddRange(
                ReflectionHelper.GetImplementedGenericTypes(
                    onServiceExposingContext.ImplementationType,
                    typeof(IObjectMapper<,>)
                ).ConvertAll(t => new ServiceIdentifier(t))
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(
            typeof(IObjectMapper<>),
            typeof(DefaultObjectMapper<>)
        );
    }
}
