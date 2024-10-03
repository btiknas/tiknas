using System.Threading.Tasks;
using Tiknas.Http.Modeling;

namespace Tiknas.Http.Client.ClientProxying;

public interface IObjectToQueryString<in TValue>
{
    Task<string> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, TValue value);
}
