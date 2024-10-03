using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.ClientProxying;
using Tiknas.Http.Modeling;
using Tiknas.TestApp.Application.Dto;

namespace Tiknas.Http.DynamicProxying;

public class TestObjectToQueryString : IObjectToQueryString<List<GetParamsNameValue>>, ITransientDependency
{
    public Task<string> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, List<GetParamsNameValue> values)
    {
        if (values.IsNullOrEmpty())
        {
            return Task.FromResult<string>(null);
        }

        var sb = new StringBuilder();

        for (var i = 0; i < values.Count; i++)
        {
            sb.Append($"NameValues[{i}].Name={values[i].Name}&NameValues[{i}].Value={values[i].Value}&");
            foreach (var item in values[i].ExtraProperties)
            {
                sb.Append($"NameValues[{i}].ExtraProperties[{item.Key}]={item.Value}&");
            }
        }

        sb.Remove(sb.Length - 1, 1);
        return Task.FromResult(sb.ToString());
    }
}
