using System.Threading.Tasks;

namespace Tiknas.TextTemplating;

public interface ITemplateContentContributor
{
    Task<string?> GetOrNullAsync(TemplateContentContributorContext context);
}
