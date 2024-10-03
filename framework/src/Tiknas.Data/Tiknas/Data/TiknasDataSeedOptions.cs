namespace Tiknas.Data;

public class TiknasDataSeedOptions
{
    public DataSeedContributorList Contributors { get; }

    public TiknasDataSeedOptions()
    {
        Contributors = new DataSeedContributorList();
    }
}
