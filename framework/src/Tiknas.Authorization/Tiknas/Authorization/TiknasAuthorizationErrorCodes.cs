namespace Tiknas.Authorization;

public static class TiknasAuthorizationErrorCodes
{
    public const string GivenPolicyHasNotGranted = "Tiknas.Authorization:010001";

    public const string GivenPolicyHasNotGrantedWithPolicyName = "Tiknas.Authorization:010002";

    public const string GivenPolicyHasNotGrantedForGivenResource = "Tiknas.Authorization:010003";

    public const string GivenRequirementHasNotGrantedForGivenResource = "Tiknas.Authorization:010004";

    public const string GivenRequirementsHasNotGrantedForGivenResource = "Tiknas.Authorization:010005";
}
