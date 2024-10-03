using System;
using System.Reflection;

namespace Tiknas.EntityFrameworkCore.GlobalFilters;

public static class TiknasEfCoreDataFilterDbFunctionMethods
{
    public const string NotSupportedExceptionMessage = "Your EF Core database provider does not support 'User-defined function mapping'." +
                                                        "Please set 'UseDbFunction' of 'TiknasEfCoreGlobalFilterOptions' to false to disable it." +
                                                        "See https://learn.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping for more information." ;

    public static bool SoftDeleteFilter(bool isDeleted, bool boolParam)
    {
        throw new NotSupportedException(NotSupportedExceptionMessage);
    }

    public static MethodInfo SoftDeleteFilterMethodInfo => typeof(TiknasEfCoreDataFilterDbFunctionMethods).GetMethod(nameof(SoftDeleteFilter))!;

    public static bool MultiTenantFilter(Guid? tenantId, Guid? currentTenantId, bool boolParam)
    {
        throw new NotSupportedException(NotSupportedExceptionMessage);
    }

    public static MethodInfo MultiTenantFilterMethodInfo => typeof(TiknasEfCoreDataFilterDbFunctionMethods).GetMethod(nameof(MultiTenantFilter))!;
}
