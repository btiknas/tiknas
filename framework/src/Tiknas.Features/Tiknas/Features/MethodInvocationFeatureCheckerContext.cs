using System.Reflection;

namespace Tiknas.Features;

public class MethodInvocationFeatureCheckerContext
{
    public MethodInfo Method { get; }

    public MethodInvocationFeatureCheckerContext(MethodInfo method)
    {
        Method = method;
    }
}
