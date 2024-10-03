using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Tiknas.AspNetCore.Mvc.ApiExploring;

public class TiknasRemoteServiceApiDescriptionProviderOptions
{
    public HashSet<ApiResponseType> SupportedResponseTypes { get; set; } = new HashSet<ApiResponseType>();
}
