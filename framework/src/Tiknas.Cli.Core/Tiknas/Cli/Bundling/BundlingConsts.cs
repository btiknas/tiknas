namespace Tiknas.Cli.Bundling;

internal static class BundlingConsts
{
    internal const string StylePlaceholderStart = "<!--TIKNAS:Styles-->";
    internal const string StylePlaceholderEnd = "<!--/TIKNAS:Styles-->";
    internal const string ScriptPlaceholderStart = "<!--TIKNAS:Scripts-->";
    internal const string ScriptPlaceholderEnd = "<!--/TIKNAS:Scripts-->";
    internal const string SupportedWebAssemblyProjectType = "Microsoft.NET.Sdk.BlazorWebAssembly";
    internal const string SupportedMauiBlazorProjectType = "Microsoft.NET.Sdk.Razor";
    internal const string WebAssembly = "webassembly";
    internal const string MauiBlazor = "maui-blazor";
}
