namespace Tiknas.AspNetCore.Mvc.AntiForgery;

public interface ITiknasAntiForgeryManager
{
    void SetCookie();

    string GenerateToken();
}
