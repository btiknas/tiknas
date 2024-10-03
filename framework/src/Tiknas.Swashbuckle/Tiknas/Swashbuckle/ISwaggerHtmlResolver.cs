using System.IO;

namespace Tiknas.Swashbuckle;

public interface ISwaggerHtmlResolver
{
    Stream Resolver();
}
