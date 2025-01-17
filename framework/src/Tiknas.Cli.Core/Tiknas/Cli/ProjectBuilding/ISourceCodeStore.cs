﻿using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Cli.ProjectBuilding;

public interface ISourceCodeStore
{
    Task<TemplateFile> GetAsync(
        string name,
        string type,
        [CanBeNull] string version = null,
        [CanBeNull] string templateSource = null,
        bool includePreReleases = false,
        bool skipCache = false,
        bool trustUserVersion = false
    );
}
