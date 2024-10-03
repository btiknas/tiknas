namespace Tiknas.ApiVersioning;

public interface IRequestedApiVersion
{
    string? Current { get; }
}
