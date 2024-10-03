using System;
using System.Collections.Generic;
using Tiknas.Collections;

namespace Tiknas.Validation;

public class TiknasValidationOptions
{
    public List<Type> IgnoredTypes { get; }

    public ITypeList<IObjectValidationContributor> ObjectValidationContributors { get; set; }

    public TiknasValidationOptions()
    {
        IgnoredTypes = new List<Type>();
        ObjectValidationContributors = new TypeList<IObjectValidationContributor>();
    }
}
