using JetBrains.Annotations;

namespace Tiknas.Settings;

public interface ISettingEncryptionService
{
    string? Encrypt([NotNull] SettingDefinition settingDefinition, string? plainValue);

    string? Decrypt([NotNull] SettingDefinition settingDefinition, string? encryptedValue);
}
