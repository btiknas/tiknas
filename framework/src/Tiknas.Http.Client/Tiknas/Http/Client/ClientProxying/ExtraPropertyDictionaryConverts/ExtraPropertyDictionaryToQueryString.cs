using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiknas.Data;
using Tiknas.DependencyInjection;
using Tiknas.Http.Modeling;

namespace Tiknas.Http.Client.ClientProxying.ExtraPropertyDictionaryConverts;

public class ExtraPropertyDictionaryToQueryString : IObjectToQueryString<ExtraPropertyDictionary>, ITransientDependency
{
    public Task<string> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, ExtraPropertyDictionary extraPropertyDictionary)
    {
        if (extraPropertyDictionary.IsNullOrEmpty())
        {
            return Task.FromResult<string>(null!);
        }

        var sb = new StringBuilder();
        foreach (var item in extraPropertyDictionary)
        {
            sb.Append($"ExtraProperties[{item.Key}]={item.Value}&");
        }
        if (sb.Length > 0)
        {
            sb.Remove(sb.Length - 1, 1);
        }

        return Task.FromResult(sb.ToString());
    }
}
