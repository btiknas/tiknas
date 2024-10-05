namespace Tiknas.Cli;

public static class CliConsts
{
    public const string Command = "TiknasCliCommand";

    public const string BranchPrefix = "branch@";

    public const string DocsLink = "https://tiknas.de/docs";

    public const string HttpClientName = "TiknasHttpClient";

    public const string GithubHttpClientName = "GithubHttpClient";

    public const string LogoutUrl = CliUrls.WwwTiknasIo + "api/license/logout";

    public const string LicenseCodePlaceHolder = @"<LICENSE_CODE/>";

    public const string AppSettingsJsonFileName = "appsettings.json";

    public const string AppSettingsSecretJsonFileName = "appsettings.secrets.json";
    
    public static class MemoryKeys
    {
        public const string LatestCliVersionCheckDate = "LatestCliVersionCheckDate";
    }
}
