using Tiknas.AspNetCore.Authentication.OAuth.Claims;
using Tiknas.Security.Claims;

namespace Microsoft.AspNetCore.Authentication.OAuth.Claims;

public static class TiknasClaimActionCollectionExtensions
{
    public static void MapTiknasClaimTypes(this ClaimActionCollection claimActions)
    {
        if (TiknasClaimTypes.UserName != "name")
        {
            claimActions.MapJsonKey(TiknasClaimTypes.UserName, "name");
            claimActions.DeleteClaim("name");
            claimActions.RemoveDuplicate(TiknasClaimTypes.UserName);
        }
        
        if (TiknasClaimTypes.Name != "given_name")
        {
            claimActions.MapJsonKey(TiknasClaimTypes.Name, "given_name");
            claimActions.DeleteClaim("given_name");
            claimActions.RemoveDuplicate(TiknasClaimTypes.Name);
        }
                
        if (TiknasClaimTypes.SurName != "family_name")
        {
            claimActions.MapJsonKey(TiknasClaimTypes.SurName, "family_name");
            claimActions.DeleteClaim("family_name");
            claimActions.RemoveDuplicate(TiknasClaimTypes.SurName);
        }

        if (TiknasClaimTypes.Email != "email")
        {
            claimActions.MapJsonKey(TiknasClaimTypes.Email, "email");
            claimActions.DeleteClaim("email");
            claimActions.RemoveDuplicate(TiknasClaimTypes.Email);
        }

        if (TiknasClaimTypes.EmailVerified != "email_verified")
        {
            claimActions.MapJsonKey(TiknasClaimTypes.EmailVerified, "email_verified");
        }

        if (TiknasClaimTypes.PhoneNumber != "phone_number")
        {
            claimActions.MapJsonKey(TiknasClaimTypes.PhoneNumber, "phone_number");
        }

        if (TiknasClaimTypes.PhoneNumberVerified != "phone_number_verified")
        {
            claimActions.MapJsonKey(TiknasClaimTypes.PhoneNumberVerified, "phone_number_verified");
        }

        if (TiknasClaimTypes.Role != "role")
        {
            claimActions.MapJsonKeyMultiple(TiknasClaimTypes.Role, "role");
        }

        claimActions.RemoveDuplicate(TiknasClaimTypes.Name);
    }

    public static void MapJsonKeyMultiple(this ClaimActionCollection claimActions, string claimType, string jsonKey)
    {
        claimActions.Add(new MultipleClaimAction(claimType, jsonKey));
    }

    public static void RemoveDuplicate(this ClaimActionCollection claimActions, string claimType)
    {
        claimActions.Add(new RemoveDuplicateClaimAction(claimType));
    }
}
