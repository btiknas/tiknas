using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.Reflection;

namespace Tiknas.Serialization;

public class TiknasSerializationModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnExposing(onServiceExposingContext =>
        {
            //Register types for IObjectSerializer<T> if implements
            onServiceExposingContext.ExposedTypes.AddRange(
                ReflectionHelper.GetImplementedGenericTypes(
                    onServiceExposingContext.ImplementationType,
                    typeof(IObjectSerializer<>)
                ).ConvertAll(t => new ServiceIdentifier(t))
            );
        });
    }
}
