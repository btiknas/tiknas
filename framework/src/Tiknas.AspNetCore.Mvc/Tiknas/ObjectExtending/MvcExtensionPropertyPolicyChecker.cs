
using System.Threading.Tasks;
using Tiknas.Authorization.Permissions;
using Tiknas.DependencyInjection;
using Tiknas.Features;
using Tiknas.GlobalFeatures;

namespace Tiknas.ObjectExtending;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ExtensionPropertyPolicyChecker))]
public class MvcExtensionPropertyPolicyChecker : ExtensionPropertyPolicyChecker
{
    protected IFeatureChecker FeatureChecker { get; }
    protected IPermissionChecker PermissionChecker { get; }

    public MvcExtensionPropertyPolicyChecker(IFeatureChecker featureChecker, IPermissionChecker permissionChecker)
    {
        FeatureChecker = featureChecker;
        PermissionChecker = permissionChecker;
    }

    protected override Task<bool> CheckGlobalFeaturesAsync(string featureName)
    {
        return Task.FromResult<bool>(GlobalFeatureManager.Instance.IsEnabled(featureName));
    }

    protected async override Task<bool> CheckFeaturesAsync(string featureName)
    {
        return await FeatureChecker.IsEnabledAsync(featureName);
    }

    protected async override Task<bool> CheckPermissionsAsync(string permissionName)
    {
        return await PermissionChecker.IsGrantedAsync(permissionName);
    }
}
