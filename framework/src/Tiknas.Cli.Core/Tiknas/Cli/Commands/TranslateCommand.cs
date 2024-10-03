using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepL;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tiknas.Cli.Args;
using Tiknas.DependencyInjection;
using Tiknas.Localization.Json;

namespace Tiknas.Cli.Commands;

public class TranslateCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "translate";

    public ILogger<TranslateCommand> Logger { get; set; }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        // Verify
        if (commandLineArgs.Options.ContainsKey(Options.Verify.Long))
        {
            await VerifyJsonAsync(currentDirectory);
            return;
        }

        var referenceCulture = commandLineArgs.Options.GetOrNull(Options.ReferenceCulture.Short, Options.ReferenceCulture.Long) ?? "en";
        var allValues = commandLineArgs.Options.ContainsKey(Options.AllValues.Short) || commandLineArgs.Options.ContainsKey(Options.AllValues.Long);

        // Apply tiknas-translation.json file
        if (commandLineArgs.Options.ContainsKey(Options.Apply.Short) || commandLineArgs.Options.ContainsKey(Options.Apply.Long))
        {
            var inputFile = Path.Combine(currentDirectory, commandLineArgs.Options.GetOrNull(Options.File.Short, Options.File.Long) ?? "tiknas-translation.json");
            await ApplyTiknasTranslateInfoAsync(currentDirectory, inputFile);
            return;
        }

        var targetCulture = commandLineArgs.Options.GetOrNull(Options.Culture.Short, Options.Culture.Long);
        if (targetCulture == null)
        {
            throw new CliUsageException("Target culture is missing!" + Environment.NewLine + Environment.NewLine + GetUsageInfo());
        }

        // Translate online
        if (commandLineArgs.Options.ContainsKey(Options.Online.Long))
        {
            var authKey = commandLineArgs.Options.GetOrNull(Options.DeepLAuthKey.Short, Options.DeepLAuthKey.Short);
            if (authKey == null)
            {
                throw new CliUsageException("DeepL auth key is missing!" + Environment.NewLine + Environment.NewLine + GetUsageInfo());
            }
            await TranslateTiknasTranslateInfoAsync(currentDirectory, targetCulture, referenceCulture, allValues, authKey);
            return;
        }

        // Generate tiknas-translation.json file
        var outputFile = Path.Combine(currentDirectory, commandLineArgs.Options.GetOrNull(Options.Output.Short, Options.Output.Long) ?? "tiknas-translation.json");
        await GenerateTiknasTranslateInfoAsync(currentDirectory, targetCulture, referenceCulture, allValues, outputFile);
    }

    private Task GenerateTiknasTranslateInfoAsync(string currentDirectory, string targetCulture, string referenceCulture, bool allValues, string outputFile)
    {
        Logger.LogInformation("Tiknas translate...");
        Logger.LogInformation("Target culture: " + targetCulture);
        Logger.LogInformation("Reference culture: " + referenceCulture);
        Logger.LogInformation("Output file: " + outputFile);
        if (allValues)
        {
            Logger.LogInformation("Include all keys");
        }

        var translateInfo = GetTiknasTranslateInfo(currentDirectory, targetCulture, referenceCulture, allValues);
        File.WriteAllText(outputFile, JsonConvert.SerializeObject(translateInfo, Formatting.Indented));
        Logger.LogInformation($"The translation file has been created.");

        return Task.CompletedTask;
    }

    private TiknasTranslateInfo GetTiknasTranslateInfo(string directory, string targetCultureName, string referenceCultureName, bool allValues)
    {
        var translateInfo = new TiknasTranslateInfo
        {
            ReferenceCulture = referenceCultureName,
            TargetCulture = targetCultureName,
            Resources = new List<TiknasTranslateResource>()
        };

        var referenceCultureFiles = GetCultureJsonFiles(directory, referenceCultureName);
        foreach (var filePath in referenceCultureFiles)
        {
            var directoryName = Path.GetDirectoryName(filePath) ?? string.Empty;

            var referenceLocalizationInfo = GetTiknasLocalizationInfoOrNull(filePath);
            if (referenceLocalizationInfo == null) // Not tiknas json file
            {
                continue;
            }

            var resource = new TiknasTranslateResource
            {
                ResourcePath = directoryName,
                Texts = new List<TiknasTranslateResourceText>()
            };

            foreach (var text in referenceLocalizationInfo.Texts)
            {
                resource.Texts.Add(new TiknasTranslateResourceText
                {
                    LocalizationKey = text.Name,
                    Reference = text.Value,
                    Target = string.Empty
                });
            }

            //Use target json file content to fill resource texts
            var targetFile = Path.Combine(directoryName, $"{targetCultureName}.json");
            if (File.Exists(targetFile))
            {
                var targetLocalizationInfo = GetTiknasLocalizationInfoOrNull(targetFile);
                foreach (var referenceResourceText in resource.Texts)
                {
                    var text = targetLocalizationInfo.Texts.FirstOrDefault(x => x.Name == referenceResourceText.LocalizationKey);
                    referenceResourceText.Target = text?.Value ?? string.Empty;
                }
            }

            if (!allValues)
            {
                //Only include missing keys.
                resource.Texts.RemoveAll(x => !x.Target.Equals(string.Empty));
            }

            if (resource.Texts.Any())
            {
                translateInfo.Resources.Add(resource);
            }
        }

        return translateInfo;
    }

    private async Task TranslateTiknasTranslateInfoAsync(string directory, string targetCulture, string referenceCulture, bool allValues, string authKey)
    {
        Logger.LogInformation("Tiknas translate online...");
        Logger.LogInformation("Target culture: " + targetCulture);
        Logger.LogInformation("Reference culture: " + referenceCulture);
        if (allValues)
        {
            Logger.LogInformation("Include all keys");
        }

        var translateInfo = GetTiknasTranslateInfo(directory, targetCulture, referenceCulture, allValues);
        foreach (var resource in translateInfo.Resources)
        {
            var targetFile = Path.Combine(resource.ResourcePath, translateInfo.TargetCulture + ".json");
            var targetLocalizationInfo = File.Exists(targetFile)
                ? GetTiknasLocalizationInfoOrNull(targetFile)
                : new TiknasLocalizationInfo()
                {
                    Culture = translateInfo.TargetCulture,
                    Texts = new List<NameValue>()
                };

            if (targetLocalizationInfo == null)
            {
                throw new CliUsageException(
                    $"Failed to get localization information from {targetFile} file." +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }

            var referenceFile = Path.Combine(resource.ResourcePath, translateInfo.ReferenceCulture + ".json");
            if (!File.Exists(referenceFile))
            {
                throw new CliUsageException(
                    $"{referenceFile} file does not exist.." +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }
            var referenceLocalizationInfo = GetTiknasLocalizationInfoOrNull(referenceFile);
            if (referenceLocalizationInfo == null)
            {
                throw new CliUsageException(
                    $"Failed to get localization information from {referenceFile} file." +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }

            var translator = new Translator(authKey);

            var texts = resource.Texts.Select(x => x.Reference);

            var translations = await translator.TranslateTextAsync(texts, await GetDeeplLanguageCode(referenceCulture), await GetDeeplLanguageCode(targetCulture));
            for (var i = 0; i < translations.Length; i++)
            {
                resource.Texts[i].Target = translations[i].Text;
            }

            foreach (var text in resource.Texts)
            {
                var targetText = targetLocalizationInfo.Texts.FirstOrDefault(x => x.Name == text.LocalizationKey);
                if (targetText != null)
                {
                    if (!text.Target.IsNullOrEmpty())
                    {
                        Logger.LogInformation($"Update translation: {targetText.Name} => " + text.Target);
                        targetText.Value = text.Target;
                    }
                }
                else
                {
                    Logger.LogInformation($"Create translation: {text.LocalizationKey} => " + text.Target);
                    targetLocalizationInfo.Texts.Add(new NameValue(text.LocalizationKey, text.Target));
                }
            }

            Logger.LogInformation($"Write translation json to {targetFile}.");

            // sort keys
            targetLocalizationInfo = SortLocalizedKeys(targetLocalizationInfo, referenceLocalizationInfo);
            File.WriteAllText(targetFile, TiknasLocalizationInfoToJsonFile(targetLocalizationInfo));
        }
    }

    private Task<string> GetDeeplLanguageCode(string tiknasCulture)
    {
        var deeplLanguages = new List<string>()
        {
            LanguageCode.Bulgarian ,
            LanguageCode.Czech ,
            LanguageCode.Danish ,
            LanguageCode.German ,
            LanguageCode.Greek ,
            LanguageCode.English ,
            LanguageCode.EnglishBritish ,
            LanguageCode.EnglishAmerican ,
            LanguageCode.Spanish ,
            LanguageCode.Estonian ,
            LanguageCode.Finnish ,
            LanguageCode.French ,
            LanguageCode.Hungarian ,
            LanguageCode.Indonesian ,
            LanguageCode.Italian ,
            LanguageCode.Japanese ,
            LanguageCode.Korean ,
            LanguageCode.Lithuanian ,
            LanguageCode.Latvian ,
            LanguageCode.Norwegian ,
            LanguageCode.Dutch ,
            LanguageCode.Polish ,
            LanguageCode.Portuguese ,
            LanguageCode.PortugueseBrazilian ,
            LanguageCode.PortugueseEuropean ,
            LanguageCode.Romanian ,
            LanguageCode.Russian ,
            LanguageCode.Slovak ,
            LanguageCode.Slovenian ,
            LanguageCode.Swedish ,
            LanguageCode.Turkish ,
            LanguageCode.Ukrainian,
            LanguageCode.Chinese
        };

        if (tiknasCulture == "zh-Hans")
        {
            return Task.FromResult(LanguageCode.Chinese);
        }

        var deeplCulture = deeplLanguages.FirstOrDefault(x => x.Equals(tiknasCulture, StringComparison.OrdinalIgnoreCase));
        if (deeplCulture == null)
        {
            throw new CliUsageException(
                $"DeepL does not support {tiknasCulture} culture." +
                Environment.NewLine + Environment.NewLine +
                GetUsageInfo()
            );
        }

        return Task.FromResult(deeplCulture);
    }

    private Task ApplyTiknasTranslateInfoAsync(string directory, string filename)
    {
        Logger.LogInformation("Tiknas translate apply...");
        Logger.LogInformation("Input file: " + filename);

        var translateJsonPath = Path.Combine(directory, filename);
        if (!File.Exists(translateJsonPath))
        {
            throw new CliUsageException(
                $"{translateJsonPath} file does not exist.." +
                Environment.NewLine + Environment.NewLine +
                GetUsageInfo()
            );
        }

        var translateInfo = GetTiknasTranslateInfo(translateJsonPath);
        foreach (var resource in translateInfo.Resources)
        {
            var targetFile = Path.Combine(resource.ResourcePath, translateInfo.TargetCulture + ".json");
            var targetLocalizationInfo = File.Exists(targetFile)
                ? GetTiknasLocalizationInfoOrNull(targetFile)
                : new TiknasLocalizationInfo()
                {
                    Culture = translateInfo.TargetCulture,
                    Texts = new List<NameValue>()
                };

            if (targetLocalizationInfo == null)
            {
                throw new CliUsageException(
                    $"Failed to get localization information from {targetFile} file." +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }

            var referenceFile = Path.Combine(resource.ResourcePath, translateInfo.ReferenceCulture + ".json");
            if (!File.Exists(referenceFile))
            {
                throw new CliUsageException(
                    $"{referenceFile} file does not exist.." +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }
            var referenceLocalizationInfo = GetTiknasLocalizationInfoOrNull(referenceFile);
            if (referenceLocalizationInfo == null)
            {
                throw new CliUsageException(
                    $"Failed to get localization information from {referenceFile} file." +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }

            foreach (var text in resource.Texts)
            {
                var targetText = targetLocalizationInfo.Texts.FirstOrDefault(x => x.Name == text.LocalizationKey);
                if (targetText != null)
                {
                    if (!text.Target.IsNullOrEmpty())
                    {
                        Logger.LogInformation($"Update translation: {targetText.Name} => " + text.Target);
                        targetText.Value = text.Target;
                    }
                }
                else
                {
                    Logger.LogInformation($"Create translation: {text.LocalizationKey} => " + text.Target);
                    targetLocalizationInfo.Texts.Add(new NameValue(text.LocalizationKey, text.Target));
                }
            }

            Logger.LogInformation($"Write translation json to {targetFile}.");

            // sort keys
            targetLocalizationInfo = SortLocalizedKeys(targetLocalizationInfo, referenceLocalizationInfo);
            File.WriteAllText(targetFile, TiknasLocalizationInfoToJsonFile(targetLocalizationInfo));

            // remove translate json file(tiknas-translation.json)
            File.Delete(translateJsonPath);
            Logger.LogInformation($"Delete the {translateJsonPath} file, if you need to translate again, please re-run the [tiknas translate] command.");
        }

        return Task.CompletedTask;
    }

    private static IEnumerable<string> GetCultureJsonFiles(string path, string cultureName = null)
    {
        var excludeDirectory = new List<string>()
        {
            "node_modules",
            "wwwroot",
            ".git",
            "bin",
            "obj"
        };

        var allCultureNames = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(x => !x.Name.IsNullOrWhiteSpace()).Select(x => x.Name).ToList();
        return Directory.GetFiles(path, "*.json", SearchOption.AllDirectories)
            .Where(file => excludeDirectory.All(x => file.IndexOf(x, StringComparison.OrdinalIgnoreCase) == -1))
            .Where(file => allCultureNames.Any(x => Path.GetFileName(file).Equals($"{x}.json", StringComparison.OrdinalIgnoreCase)))
            .WhereIf(!cultureName.IsNullOrWhiteSpace(), jsonFile => Path.GetFileName(jsonFile).Equals($"{cultureName}.json", StringComparison.OrdinalIgnoreCase));
    }

    private TiknasLocalizationInfo GetTiknasLocalizationInfoOrNull(string path)
    {
        if (!File.Exists(path))
        {
            throw new CliUsageException(
                $"File {path} does not exist!" +
                Environment.NewLine + Environment.NewLine +
                GetUsageInfo()
            );
        }

        var json = File.ReadAllText(path);
        JObject jObject;
        try
        {
            jObject = JObject.Parse(json);
        }
        catch (Exception)
        {
            return null;
        }

        var culture = jObject.GetValue("culture") ?? jObject.GetValue("Culture");
        var texts = jObject.GetValue("texts") ?? jObject.GetValue("Texts");
        if (culture == null || texts == null)
        {
            return null;
        }

        var localizationInfo = new TiknasLocalizationInfo
        {
            Culture = culture.Value<string>(),
            Texts = new List<NameValue>()
        };

        foreach (var text in texts)
        {
            var property = (text as JProperty);
            localizationInfo.Texts.Add(new NameValue(property?.Name, property?.Value.Value<string>()));
        }

        return localizationInfo;
    }

    private static string TiknasLocalizationInfoToJsonFile(TiknasLocalizationInfo localizationInfo)
    {
        var jObject = new JObject { { "culture", localizationInfo.Culture } };
        var value = new JObject();
        foreach (var text in localizationInfo.Texts)
        {
            value.Add(text.Name, text.Value);
        }
        jObject.Add("texts", value);
        return jObject.ToString();
    }

    private TiknasTranslateInfo GetTiknasTranslateInfo(string path)
    {
        if (!File.Exists(path))
        {
            throw new CliUsageException(
                $"File {path} does not exist!" +
                Environment.NewLine + Environment.NewLine +
                GetUsageInfo()
            );
        }

        TiknasTranslateInfo translateInfo;
        try
        {
            translateInfo = JsonConvert.DeserializeObject<TiknasTranslateInfo>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            throw new CliUsageException(
                e.Message +
                Environment.NewLine + Environment.NewLine +
                GetUsageInfo()
            );
        }

        return translateInfo;
    }

    private Task VerifyJsonAsync(string currentDirectory)
    {
        var jsonFiles = GetCultureJsonFiles(currentDirectory);
        var hasInvalidJsonFile = false;
        foreach (var jsonFile in jsonFiles)
        {
            try
            {
                var jsonString = File.ReadAllText(jsonFile);
                _ = JsonLocalizationDictionaryBuilder.BuildFromJsonString(jsonString);
            }
            catch (Exception)
            {
                Logger.LogError($"Invalid json file: {jsonFile}");
                hasInvalidJsonFile = true;
            }
        }

        Logger.LogInformation(!hasInvalidJsonFile ? "All json files are valid." : "Some json files are invalid.");

        return Task.CompletedTask;
    }

    private static TiknasLocalizationInfo SortLocalizedKeys(TiknasLocalizationInfo targetLocalizationInfo, TiknasLocalizationInfo referenceLocalizationInfo)
    {
        var sortedLocalizationInfo = new TiknasLocalizationInfo
        {
            Culture = targetLocalizationInfo.Culture,
            Texts = new List<NameValue>()
        };

        foreach (var targetText in referenceLocalizationInfo.Texts.Select(text =>
            targetLocalizationInfo.Texts.FirstOrDefault(x => x.Name == text.Name))
            .Where(targetText => targetText != null))
        {
            sortedLocalizationInfo.Texts.Add(targetText);
        }

        return sortedLocalizationInfo;
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas translate [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("");
        sb.AppendLine("--culture|-c <culture>                       Target culture. eg: zh-Hans");
        sb.AppendLine("--reference-culture|-r <culture>             Default: en");
        sb.AppendLine("--output|-o <file-name>                      Output file name, Default tiknas-translation.json");
        sb.AppendLine("--all-values|-all                            Include all keys. Default false");
        sb.AppendLine("--apply|-a                                   Creates or updates the file for the translated culture.");
        sb.AppendLine("--file|-f <file-name>                        Default: tiknas-translation.json");
        sb.AppendLine("--online                                     Translate online.");
        sb.AppendLine("--deepl-auth-key <auth-key>                  DeepL auth key for online translation.");
        sb.AppendLine("--verify                                     Verify that all localized files are correct JSON format.");
        sb.AppendLine("");
        sb.AppendLine("Examples:");
        sb.AppendLine("");
        sb.AppendLine("  tiknas translate -c zh-Hans");
        sb.AppendLine("  tiknas translate -c zh-Hans -r en");
        sb.AppendLine("  tiknas translate --apply");
        sb.AppendLine("  tiknas translate -a -f my-translation.json");
        sb.AppendLine("  tiknas translate -c zh-Hans --online --deepl-auth-key <auth-key>");
        sb.AppendLine("  tiknas translate -c zh-Hans -r tr --online --deepl-auth-key <auth-key>");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.io/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Mainly used to translate TIKNAS's resources (JSON files) easier.";
    }

    public static class Options
    {
        public static class Culture
        {
            public const string Short = "c";
            public const string Long = "culture";
        }

        public static class ReferenceCulture
        {
            public const string Short = "r";
            public const string Long = "reference-culture";
        }

        public static class Output
        {
            public const string Short = "o";
            public const string Long = "output";
        }

        public static class AllValues
        {
            public const string Short = "all";
            public const string Long = "all-values";
        }

        public static class Apply
        {
            public const string Short = "a";
            public const string Long = "apply";
        }

        public static class File
        {
            public const string Short = "f";
            public const string Long = "file";
        }

        public static class Online
        {
            public const string Long = "online";
        }

        public static class DeepLAuthKey
        {
            public const string Short = "deepl-auth-key";
        }

        public static class Verify
        {
            public const string Long = "verify";
        }
    }

    public class TiknasTranslateInfo
    {
        public string ReferenceCulture { get; set; }

        public string TargetCulture { get; set; }

        public List<TiknasTranslateResource> Resources { get; set; }
    }

    public class TiknasTranslateResource
    {
        public string ResourcePath { get; set; }

        public List<TiknasTranslateResourceText> Texts { get; set; }
    }

    public class TiknasTranslateResourceText
    {
        public string LocalizationKey { get; set; }

        public string Reference { get; set; }

        public string Target { get; set; }
    }

    public class TiknasLocalizationInfo
    {
        public string Culture { get; set; }

        public List<NameValue> Texts { get; set; }
    }
}
