using System;
using Tiknas.ExceptionHandling;

namespace Tiknas.GlobalFeatures;

[Serializable]
public class TiknasGlobalFeatureNotEnabledException : TiknasException, IHasErrorCode
{
    public string? Code { get; }

    public TiknasGlobalFeatureNotEnabledException(string? message = null, string? code = null, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
    }

    public TiknasGlobalFeatureNotEnabledException WithData(string name, object value)
    {
        Data[name] = value;
        return this;
    }
}
