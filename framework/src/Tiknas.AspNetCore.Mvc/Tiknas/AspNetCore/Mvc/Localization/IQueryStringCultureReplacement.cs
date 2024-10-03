using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Mvc.Localization;

public interface IQueryStringCultureReplacement
{
    Task ReplaceAsync(QueryStringCultureReplacementContext context);
}
