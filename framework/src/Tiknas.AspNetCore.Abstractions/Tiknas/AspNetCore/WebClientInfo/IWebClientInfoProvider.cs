namespace Tiknas.AspNetCore.WebClientInfo;

public interface IWebClientInfoProvider
{
    string? BrowserInfo { get; }

    string? ClientIpAddress { get; }

    string? DeviceInfo { get; }
}
