using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.ExceptionHandling;

public interface IExceptionNotifier
{
    Task NotifyAsync([NotNull] ExceptionNotificationContext context);
}
