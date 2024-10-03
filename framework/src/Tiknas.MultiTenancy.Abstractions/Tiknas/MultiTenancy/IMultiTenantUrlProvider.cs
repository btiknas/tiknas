using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.MultiTenancy;

public interface IMultiTenantUrlProvider
{
    Task<string> GetUrlAsync(string templateUrl);
}