namespace Tiknas.AspNetCore.Components.Web;

public class TiknasAuthenticationOptions
{
    public string LoginUrl { get; set; } = "Account/Login";

    public string LogoutUrl { get; set; } = "Account/Logout"; 
}