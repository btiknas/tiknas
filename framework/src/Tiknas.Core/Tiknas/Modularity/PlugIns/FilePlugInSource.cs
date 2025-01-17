﻿using System;
using System.Collections.Generic;
using System.Runtime.Loader;

namespace Tiknas.Modularity.PlugIns;

public class FilePlugInSource : IPlugInSource
{
    public string[] FilePaths { get; }

    public FilePlugInSource(params string[]? filePaths)
    {
        FilePaths = filePaths ?? new string[0];
    }

    public Type[] GetModules()
    {
        var modules = new List<Type>();

        foreach (var filePath in FilePaths)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);

            try
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (TiknasModule.IsTiknasModule(type))
                    {
                        modules.AddIfNotContains(type);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TiknasException("Could not get module types from assembly: " + assembly.FullName, ex);
            }
        }

        return modules.ToArray();
    }
}
