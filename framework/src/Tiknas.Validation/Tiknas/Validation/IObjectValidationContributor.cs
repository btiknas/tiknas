using System.Threading.Tasks;

namespace Tiknas.Validation;

public interface IObjectValidationContributor
{
    Task AddErrorsAsync(ObjectValidationContext context);
}
