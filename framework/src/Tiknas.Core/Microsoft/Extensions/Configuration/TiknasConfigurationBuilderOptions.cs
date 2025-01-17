﻿using System.Reflection;

namespace Microsoft.Extensions.Configuration;

public class TiknasConfigurationBuilderOptions
{
    /// <summary>
    /// Used to set assembly which is used to get the user secret id for the application.
    /// Use this or <see cref="UserSecretsId"/> (higher priority)
    /// </summary>
    public Assembly? UserSecretsAssembly { get; set; }

    /// <summary>
    /// Used to set user secret id for the application.
    /// Use this (higher priority) or <see cref="UserSecretsAssembly"/>
    /// </summary>
    public string? UserSecretsId { get; set; }

    /// <summary>
    /// Default value: "appsettings".
    /// </summary>
    public string FileName { get; set; } = "appsettings";

    /// <summary>
    /// Whether the file is optional, Default value: true.
    /// </summary>
    public bool Optional { get; set; } = true;

    /// <summary>
    /// Whether the configuration should be reloaded if the file changes, Default value: true.
    /// </summary>
    public bool ReloadOnChange { get; set; } = true;

    /// <summary>
    /// Environment name. Generally used "Development", "Staging" or "Production".
    /// </summary>
    public string? EnvironmentName { get; set; }

    /// <summary>
    /// Base path to read the configuration file indicated by <see cref="FileName"/>.
    /// </summary>
    public string? BasePath { get; set; }

    /// <summary>
    /// Prefix for the environment variables.
    /// </summary>
    public string? EnvironmentVariablesPrefix { get; set; }

    /// <summary>
    /// Command line arguments.
    /// </summary>
    public string[]? CommandLineArgs { get; set; }
}
