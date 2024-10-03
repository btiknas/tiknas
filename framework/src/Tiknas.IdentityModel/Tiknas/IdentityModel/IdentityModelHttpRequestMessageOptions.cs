using System;
using System.Net.Http;

namespace Tiknas.IdentityModel;

public class IdentityModelHttpRequestMessageOptions
{
    public Action<HttpRequestMessage>? ConfigureHttpRequestMessage { get; set; }
}
