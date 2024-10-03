using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.ExceptionHandling;

public interface IUserExceptionInformer
{
    void Inform(UserExceptionInformerContext context);

    Task InformAsync(UserExceptionInformerContext context);
}
