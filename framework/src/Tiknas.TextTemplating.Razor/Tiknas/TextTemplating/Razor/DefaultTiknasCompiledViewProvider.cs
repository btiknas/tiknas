﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.Threading;

namespace Tiknas.TextTemplating.Razor;

public class DefaultTiknasCompiledViewProvider : ITiknasCompiledViewProvider
    , ITransientDependency
{
    private readonly static ConcurrentDictionary<string, Assembly> CachedAssembles = new ConcurrentDictionary<string, Assembly>();
    private readonly static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);

    private readonly TiknasCompiledViewProviderOptions _options;
    private readonly TiknasRazorTemplateCSharpCompiler _razorTemplateCSharpCompiler;
    private readonly ITiknasRazorProjectEngineFactory _razorProjectEngineFactory;
    private readonly ITemplateContentProvider _templateContentProvider;

    public DefaultTiknasCompiledViewProvider(
        IOptions<TiknasCompiledViewProviderOptions> options,
        ITiknasRazorProjectEngineFactory razorProjectEngineFactory,
        TiknasRazorTemplateCSharpCompiler razorTemplateCSharpCompiler,
        ITemplateContentProvider templateContentProvider)
    {
        _options = options.Value;

        _razorProjectEngineFactory = razorProjectEngineFactory;
        _razorTemplateCSharpCompiler = razorTemplateCSharpCompiler;
        _templateContentProvider = templateContentProvider;
    }

    public virtual async Task<Assembly> GetAssemblyAsync(TemplateDefinition templateDefinition)
    {
        async Task<Assembly> CreateAssembly(string content)
        {
            using (var assemblyStream = await GetAssemblyStreamAsync(templateDefinition, content))
            {
                return Assembly.Load(await assemblyStream.GetAllBytesAsync());
            }
        }

        var templateContent = await _templateContentProvider.GetContentOrNullAsync(templateDefinition);
        if (templateContent == null)
        {
            throw new TiknasException($"Razor template content of {templateDefinition.Name} is null!");
        }

        using (await SemaphoreSlim.LockAsync())
        {
            var cacheKey = (templateDefinition.Name + templateContent).ToMd5();
            if (CachedAssembles.TryGetValue(cacheKey, out var cachedAssemble))
            {
                return cachedAssemble;
            }
            var assemble = await CreateAssembly(templateContent);
            CachedAssembles.TryAdd(cacheKey, assemble);
            return assemble;
        }
    }

    protected virtual async Task<Stream> GetAssemblyStreamAsync(TemplateDefinition templateDefinition, string templateContent)
    {
        var razorProjectEngine = await _razorProjectEngineFactory.CreateAsync(builder =>
        {
            builder.SetNamespace(TiknasRazorTemplateConsts.DefaultNameSpace);
            builder.ConfigureClass((document, node) =>
            {
                node.ClassName = TiknasRazorTemplateConsts.DefaultClassName;
            });
        });

        var codeDocument = razorProjectEngine.Process(
            RazorSourceDocument.Create(templateContent, templateDefinition.Name), null,
            new List<RazorSourceDocument>(), new List<TagHelperDescriptor>());

        var cSharpDocument = codeDocument.GetCSharpDocument();

        var templateReferences = _options.TemplateReferences
            .GetOrDefault(templateDefinition.Name)
            ?.Select(x => x)
            .Cast<MetadataReference>()
            .ToList();

        return _razorTemplateCSharpCompiler.CreateAssembly(cSharpDocument.GeneratedCode, templateDefinition.Name, templateReferences);
    }
}
