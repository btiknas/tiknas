using System.Threading.Tasks;

namespace Tiknas.Features;

public interface IMethodFeatureTestService
{
    Task<int> Feature1Async();

    Task NonFeatureAsync();
}
