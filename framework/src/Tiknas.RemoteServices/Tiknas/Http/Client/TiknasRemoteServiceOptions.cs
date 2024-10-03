namespace Tiknas.Http.Client;

public class TiknasRemoteServiceOptions
{
    public RemoteServiceConfigurationDictionary RemoteServices { get; set; }

    public TiknasRemoteServiceOptions()
    {
        RemoteServices = new RemoteServiceConfigurationDictionary();
    }
}
