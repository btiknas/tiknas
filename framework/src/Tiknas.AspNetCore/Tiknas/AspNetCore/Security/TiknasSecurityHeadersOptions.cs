using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tiknas.AspNetCore.Security;

public class TiknasSecurityHeadersOptions
{
    public bool UseContentSecurityPolicyHeader { get; set; }
    
    public bool UseContentSecurityPolicyScriptNonce { get; set; }
    
    public string? ContentSecurityPolicyValue { get; set; }

    public Dictionary<string, string> Headers { get; }
    
    public List<Func<HttpContext, Task<bool>>> IgnoredScriptNonceSelectors { get; }
    
    public List<string> IgnoredScriptNoncePaths { get; }

    public TiknasSecurityHeadersOptions()
    {
        Headers = new Dictionary<string, string>();
        IgnoredScriptNonceSelectors = new List<Func<HttpContext, Task<bool>>>();
        IgnoredScriptNoncePaths = new List<string>();
    }
}
