using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.ClientProxying;
using Tiknas.Http.Modeling;

namespace Tiknas.Http.DynamicProxying;

public class TestObjectToPath : IObjectToPath<int>, ITransientDependency
{
    public Task<string> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, int value)
    {
        if (actionApiDescription.Name == nameof(IRegularTestController.GetObjectandCountAsync))
        {
            if (value <= 0)
            {
                value = 888;
            }
            return Task.FromResult(value.ToString());
        }

        return Task.FromResult<string>(null);
    }
}
