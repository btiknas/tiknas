using System.Threading.Tasks;

namespace Tiknas.Validation;

public interface IMethodInvocationValidator
{
    Task ValidateAsync(MethodInvocationValidationContext context);
}
