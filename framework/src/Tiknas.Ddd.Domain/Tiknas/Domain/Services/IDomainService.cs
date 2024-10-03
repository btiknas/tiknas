using Tiknas.DependencyInjection;

namespace Tiknas.Domain.Services;

/// <summary>
/// This interface can be implemented by all domain services to identify them by convention.
/// </summary>
public interface IDomainService : ITransientDependency
{

}
