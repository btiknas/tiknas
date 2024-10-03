using System;
using System.Collections.Generic;
using NUglify;

namespace Tiknas.Minify.NUglify;

public class NUglifyException : TiknasException
{
    public List<UglifyError>? Errors { get; set; }

    public NUglifyException(string message, List<UglifyError> errors)
        : base(message)
    {
        Errors = errors;
    }

    public NUglifyException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
