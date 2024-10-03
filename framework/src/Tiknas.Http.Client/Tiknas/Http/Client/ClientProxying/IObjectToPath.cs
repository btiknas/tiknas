using System.Threading.Tasks;
using Tiknas.Http.Modeling;

namespace Tiknas.Http.Client.ClientProxying
{
    public interface IObjectToPath<in TValue>
    {
        Task<string> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, TValue value);
    }
}
