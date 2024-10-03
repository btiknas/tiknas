using System.Collections.Generic;

namespace Tiknas.AspNetCore.Uow;

public class AspNetCoreUnitOfWorkTransactionBehaviourProviderOptions
{
    public List<string> NonTransactionalUrls { get; }

    public AspNetCoreUnitOfWorkTransactionBehaviourProviderOptions()
    {
        NonTransactionalUrls = new List<string>
            {
                "/connect/"
            };
    }
}
