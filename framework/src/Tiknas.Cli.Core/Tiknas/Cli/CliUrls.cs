using System;
using Tiknas.Http.Modeling;

namespace Tiknas.Cli;

public static class CliUrls
{
    public const string WwwTiknasIo = WwwTiknasIoProduction;
    public const string AccountTiknasIo = AccountTiknasIoProduction;
    public const string NuGetRootPath = NuGetRootPathProduction;
    public const string LatestVersionCheckFullPath =
        "https://raw.githubusercontent.com/tiknasframework/tiknas/dev/latest-versions.json";

    public const string WwwTiknasIoProduction = "https://tiknas.io/";
    public const string AccountTiknasIoProduction = "https://account.tiknas.io/";
    public const string NuGetRootPathProduction = "https://nuget.tiknas.io/";

    public const string WwwTiknasIoDevelopment = "https://localhost:44328/";
    public const string AccountTiknasIoDevelopment = "https://localhost:44333/";
    public const string NuGetRootPathDevelopment = "https://localhost:44373/";

    public static string GetNuGetServiceIndexUrl(string apiKey)
    {
        return $"{NuGetRootPath}{apiKey}/v3/index.json";
    }

    public static string GetNuGetPackageInfoUrl(string apiKey, string packageId)
    {
        return $"{NuGetRootPath}{apiKey}/v3/package/{packageId}/index.json";
    }

    public static string GetNuGetPackageSearchUrl(string apiKey, string packageId)
    {
        return $"{NuGetRootPath}{apiKey}/v3/search?q={packageId}";
    }

    public static string GetApiDefinitionUrl(string url, ApplicationApiDescriptionModelRequestDto model = null)
    {
        url = url.EnsureEndsWith('/');
        return $"{url}api/tiknas/api-definition{(model != null ? model.IncludeTypes ? "?includeTypes=true" : string.Empty : string.Empty)}";
    }
}
