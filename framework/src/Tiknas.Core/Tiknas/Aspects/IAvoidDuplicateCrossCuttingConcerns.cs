using System.Collections.Generic;

namespace Tiknas.Aspects;

public interface IAvoidDuplicateCrossCuttingConcerns
{
    List<string> AppliedCrossCuttingConcerns { get; }
}
