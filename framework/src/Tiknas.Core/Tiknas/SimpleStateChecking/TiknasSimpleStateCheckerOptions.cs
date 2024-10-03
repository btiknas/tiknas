using Tiknas.Collections;

namespace Tiknas.SimpleStateChecking;

public class TiknasSimpleStateCheckerOptions<TState>
    where TState : IHasSimpleStateCheckers<TState>
{
    public ITypeList<ISimpleStateChecker<TState>> GlobalStateCheckers { get; }

    public TiknasSimpleStateCheckerOptions()
    {
        GlobalStateCheckers = new TypeList<ISimpleStateChecker<TState>>();
    }
}
