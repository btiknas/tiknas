﻿using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tiknas.AspNetCore.Mvc.Versioning.App;

[ApiVersion("1.0")]
[ApiVersion("2.0")]
[ApiController]
[Route("api/v{apiVersion:apiVersion}/[controller]")]
public class HelloController : TiknasController, IHelloController
{
    [HttpGet]
    public Task<string> GetAsync()
    {
        return Task.FromResult($"Get-{HttpContext.GetRequestedApiVersion().ToString()}");
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    public Task<string> PostAsyncV1()
    {
        return PostAsync();
    }

    [HttpPost]
    [MapToApiVersion("2.0")]
    public Task<string> PostAsyncV2()
    {
        return PostAsync();
    }

    private Task<string> PostAsync()
    {
        return Task.FromResult($"Post-{HttpContext.GetRequestedApiVersion().ToString()}");
    }
}
