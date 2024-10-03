using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tiknas.Http.Modeling;

namespace Tiknas.Http.Client.ClientProxying;

public interface IObjectToFormData<in TValue>
{
    Task<List<KeyValuePair<string, HttpContent>>> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, TValue value);
}
