using System.Threading.Tasks;

namespace Tiknas.Ldap;

public interface ILdapSettingProvider
{
    public Task<bool> GetLdapOverSsl();

    public Task<string?> GetServerHostAsync();

    public Task<int> GetServerPortAsync();

    public Task<string?> GetBaseDcAsync();

    public Task<string?> GetDomainAsync();

    public Task<string?> GetUserNameAsync();

    public Task<string?> GetPasswordAsync();
}
