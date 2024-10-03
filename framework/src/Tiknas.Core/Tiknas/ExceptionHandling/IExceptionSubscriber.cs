using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.ExceptionHandling;

public interface IExceptionSubscriber
{
    Task HandleAsync([NotNull] ExceptionNotificationContext context);
}
