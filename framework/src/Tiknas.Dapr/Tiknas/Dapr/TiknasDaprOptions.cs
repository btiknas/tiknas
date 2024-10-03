namespace Tiknas.Dapr;

public class TiknasDaprOptions
{
    public string? HttpEndpoint { get; set; }

    public string? GrpcEndpoint { get; set; }
    
    public string? DaprApiToken { get; set; }
    
    public string? AppApiToken { get; set; }
}