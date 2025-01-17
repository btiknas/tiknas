﻿using System.Net;
using System.Net.Http;

namespace Tiknas.Cli.Http;

public class CliHttpClientHandler : HttpClientHandler
{
    public CliHttpClientHandler()
    {
        Proxy = WebRequest.GetSystemWebProxy();
        DefaultProxyCredentials = CredentialCache.DefaultCredentials;
    }
}
