using System.Threading.Tasks;

namespace Tiknas.Ldap;

public interface ILdapManager
{
    Task<bool> AuthenticateAsync(string username, string password);
}
