using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Components.Web.Extensibility;

public interface ILookupApiRequestService
{
    Task<string> SendAsync([NotNull] string url);
}
