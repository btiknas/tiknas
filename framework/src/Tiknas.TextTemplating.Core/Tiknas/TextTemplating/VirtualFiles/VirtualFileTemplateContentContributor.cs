﻿using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.TextTemplating.VirtualFiles;

public class VirtualFileTemplateContentContributor : ITemplateContentContributor, ITransientDependency
{
    public const string VirtualPathPropertyName = "VirtualPath";

    private readonly ILocalizedTemplateContentReaderFactory _localizedTemplateContentReaderFactory;

    public VirtualFileTemplateContentContributor(
        ILocalizedTemplateContentReaderFactory localizedTemplateContentReaderFactory)
    {
        _localizedTemplateContentReaderFactory = localizedTemplateContentReaderFactory;
    }

    public virtual async Task<string?> GetOrNullAsync(TemplateContentContributorContext context)
    {
        var localizedReader = await _localizedTemplateContentReaderFactory
            .CreateAsync(context.TemplateDefinition);

        return localizedReader.GetContentOrNull(
            context.Culture
        );
    }
}
