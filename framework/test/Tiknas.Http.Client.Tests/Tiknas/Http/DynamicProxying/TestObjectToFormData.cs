using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.ClientProxying;
using Tiknas.Http.Modeling;
using Tiknas.TestApp.Application.Dto;

namespace Tiknas.Http.DynamicProxying;

public class TestObjectToFormData : IObjectToFormData<List<GetParamsNameValue>>, ITransientDependency
{
    public Task<List<KeyValuePair<string, HttpContent>>> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, List<GetParamsNameValue> values)
    {
        if (values.IsNullOrEmpty())
        {
            return Task.FromResult<List<KeyValuePair<string, HttpContent>>>(null);
        }

        var formDataContents = new List<KeyValuePair<string, HttpContent>>();
        for (var i = 0; i < values.Count; i++)
        {
            formDataContents.Add(new KeyValuePair<string, HttpContent>($"NameValues[{i}].Name", new StringContent(values[i].Name, Encoding.UTF8)));
            formDataContents.Add(new KeyValuePair<string, HttpContent>($"NameValues[{i}].Value", new StringContent(values[i].Value, Encoding.UTF8)));

            foreach (var item in values[i].ExtraProperties)
            {
                formDataContents.Add(new KeyValuePair<string, HttpContent>($"NameValues[{i}].ExtraProperties[{item.Key}]", new StringContent(item.Value!.ToString()!, Encoding.UTF8)));
            }
        }

        return Task.FromResult(formDataContents);
    }
}
