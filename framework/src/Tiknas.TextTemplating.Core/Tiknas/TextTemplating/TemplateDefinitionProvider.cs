﻿using Tiknas.DependencyInjection;

namespace Tiknas.TextTemplating;

public abstract class TemplateDefinitionProvider : ITemplateDefinitionProvider, ITransientDependency
{
    public virtual void PreDefine(ITemplateDefinitionContext context)
    {

    }

    public abstract void Define(ITemplateDefinitionContext context);

    public virtual void PostDefine(ITemplateDefinitionContext context)
    {

    }
}
