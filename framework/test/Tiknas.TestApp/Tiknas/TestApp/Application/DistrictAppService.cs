using System.Linq;
using System.Threading.Tasks;
using Tiknas.TestApp.Domain;
using Tiknas.Application.Services;
using Tiknas.Domain.Repositories;
using Tiknas.TestApp.Application.Dto;

namespace Tiknas.TestApp.Application;

//This is especially used to test the AbstractKeyCrudAppService
public class DistrictAppService : AbstractKeyCrudAppService<District, DistrictDto, DistrictKey>
{
    public DistrictAppService(IRepository<District> repository)
        : base(repository)
    {
    }

    protected override async Task DeleteByIdAsync(DistrictKey id)
    {
        await Repository.DeleteAsync(d => d.CityId == id.CityId && d.Name == id.Name);
    }

    protected override async Task<District> GetEntityByIdAsync(DistrictKey id)
    {
        return await AsyncExecuter.FirstOrDefaultAsync(
             (await Repository.GetQueryableAsync()).Where(d => d.CityId == id.CityId && d.Name == id.Name)
        );
    }
}
