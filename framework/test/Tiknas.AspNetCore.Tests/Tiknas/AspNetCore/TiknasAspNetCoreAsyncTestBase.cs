﻿using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Shouldly;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Modularity;
using Xunit;

namespace Tiknas.AspNetCore;

public abstract class TiknasAspNetCoreAsyncTestBase<TModule> : TiknasAspNetCoreAsyncIntegratedTestBase<TModule>, IAsyncLifetime
    where TModule : TiknasModule
{
    protected virtual async Task<T> GetResponseAsObjectAsync<T>(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        var strResponse = await GetResponseAsStringAsync(url, expectedStatusCode);
        return JsonSerializer.Deserialize<T>(strResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }

    protected virtual async Task<string> GetResponseAsStringAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        using (var response = await GetResponseAsync(url, expectedStatusCode))
        {
            return await response.Content.ReadAsStringAsync();
        }
    }

    protected virtual async Task<HttpResponseMessage> GetResponseAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, bool xmlHttpRequest = false)
    {
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
        {
            requestMessage.Headers.Add("Accept-Language", CultureInfo.CurrentUICulture.Name);
            if (xmlHttpRequest)
            {
                requestMessage.Headers.Add(HeaderNames.XRequestedWith, "XMLHttpRequest");
            }
            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.ShouldBe(expectedStatusCode);
            return response;
        }
    }
}
