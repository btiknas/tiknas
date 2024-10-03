using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.DynamicProxy;

namespace Tiknas.Validation;

public class ValidationInterceptor : TiknasInterceptor, ITransientDependency
{
    private readonly IMethodInvocationValidator _methodInvocationValidator;

    public ValidationInterceptor(IMethodInvocationValidator methodInvocationValidator)
    {
        _methodInvocationValidator = methodInvocationValidator;
    }

    public override async Task InterceptAsync(ITiknasMethodInvocation invocation)
    {
        await ValidateAsync(invocation);
        await invocation.ProceedAsync();
    }

    protected virtual async Task ValidateAsync(ITiknasMethodInvocation invocation)
    {
        await _methodInvocationValidator.ValidateAsync(
            new MethodInvocationValidationContext(
                invocation.TargetObject,
                invocation.Method,
                invocation.Arguments
            )
        );
    }
}
