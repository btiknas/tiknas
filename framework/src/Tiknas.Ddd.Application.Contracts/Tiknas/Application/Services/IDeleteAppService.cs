using System.Threading.Tasks;

namespace Tiknas.Application.Services;

public interface IDeleteAppService<in TKey> : IApplicationService
{
    Task DeleteAsync(TKey id);
}
