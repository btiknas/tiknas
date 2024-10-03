using JetBrains.Annotations;
using Tiknas.Localization;
using Tiknas.MultiTenancy;

namespace Tiknas.Authorization.Permissions;

public interface ICanAddChildPermission
{
    PermissionDefinition AddPermission(
        [NotNull] string name,
        ILocalizableString? displayName = null,
        MultiTenancySides multiTenancySide = MultiTenancySides.Both,
        bool isEnabled = true);
}