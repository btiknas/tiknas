using System.Collections.Generic;
using Tiknas.ObjectExtending;

namespace Tiknas.TestApp.Application.Dto;

public class GetParamsInput : ExtensibleObject
{
    public List<GetParamsNameValue> NameValues { get; set; }

    public GetParamsNameValue NameValue { get; set; }
}

public class GetParamsNameValue : ExtensibleObject
{
    public string Name { get; set; }

    public string Value { get; set; }
}
