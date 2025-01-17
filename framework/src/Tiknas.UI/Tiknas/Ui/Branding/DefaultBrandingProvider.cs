﻿using Tiknas.DependencyInjection;

namespace Tiknas.Ui.Branding;

public class DefaultBrandingProvider : IBrandingProvider, ITransientDependency
{
    public virtual string AppName => "MyApplication";

    public virtual string? LogoUrl => null;

    public virtual string? LogoReverseUrl => null;
}
