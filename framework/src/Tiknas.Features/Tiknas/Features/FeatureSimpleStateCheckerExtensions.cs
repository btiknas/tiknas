using JetBrains.Annotations;
using Tiknas.SimpleStateChecking;

namespace Tiknas.Features;

public static class FeatureSimpleStateCheckerExtensions
{
    public static TState RequireFeatures<TState>(
        [NotNull] this TState state,
        params string[] features)
        where TState : IHasSimpleStateCheckers<TState>
    {
        state.RequireFeatures(true, features);
        return state;
    }

    public static TState RequireFeatures<TState>(
        [NotNull] this TState state,
        bool requiresAll,
        params string[] features)
        where TState : IHasSimpleStateCheckers<TState>
    {
        Check.NotNull(state, nameof(state));
        Check.NotNullOrEmpty(features, nameof(features));

        state.StateCheckers.Add(new RequireFeaturesSimpleStateChecker<TState>(requiresAll, features));
        return state;
    }
}
