namespace Tiknas.Dapr;

public interface IDaprApiTokenProvider
{
    string? GetDaprApiToken();

    string? GetAppApiToken();
}
