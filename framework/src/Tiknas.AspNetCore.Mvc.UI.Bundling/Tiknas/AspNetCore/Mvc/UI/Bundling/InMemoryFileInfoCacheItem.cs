using System;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling;

[Serializable]
[IgnoreMultiTenancy]
public class InMemoryFileInfoCacheItem
{
    public InMemoryFileInfoCacheItem(string dynamicPath, byte[] fileContent, string name)
    {
        DynamicPath = dynamicPath;
        Name = name;
        FileContent = fileContent;
    }

    public string DynamicPath { get; set; }

    public string Name { get; set; }

    public byte[] FileContent { get; set; }
}
