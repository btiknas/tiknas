using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Tiknas;
using Tiknas.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore.GlobalFilters;
using Tiknas.MultiTenancy;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasEfCoreModelBuilderExtensions
{
    public static ModelBuilder ConfigureSoftDeleteDbFunction(this ModelBuilder modelBuilder, MethodInfo methodInfo, TiknasEfCoreCurrentDbContext tiknasEfCoreCurrentDbContext)
    {
        modelBuilder.HasDbFunction(methodInfo)
            .HasTranslation(args =>
            {
                // (bool isDeleted, bool boolParam)
                var isDeleted = args[0];
                var boolParam = args[1];

                if (tiknasEfCoreCurrentDbContext.Context?.DataFilter.IsEnabled<ISoftDelete>() == true)
                {
                    // IsDeleted == false
                    return new SqlBinaryExpression(
                        ExpressionType.Equal,
                        isDeleted,
                        new SqlConstantExpression(false, typeof(bool), boolParam.TypeMapping),
                        boolParam.Type,
                        boolParam.TypeMapping);
                }

                // empty where sql
                return new SqlConstantExpression(true, typeof(bool), boolParam.TypeMapping);
            });

        return modelBuilder;
    }

    public static ModelBuilder ConfigureMultiTenantDbFunction(this ModelBuilder modelBuilder, MethodInfo methodInfo, TiknasEfCoreCurrentDbContext tiknasEfCoreCurrentDbContext)
    {
        modelBuilder.HasDbFunction(methodInfo)
            .HasTranslation(args =>
            {
                // (Guid? tenantId, int? currentTenantId)
                var tenantId = args[0];
                var currentTenantId = args[1];
                var boolParam = args[2];

                if (tiknasEfCoreCurrentDbContext.Context?.DataFilter.IsEnabled<IMultiTenant>() == true)
                {
                    // TenantId == CurrentTenantId
                    return new SqlBinaryExpression(
                        ExpressionType.Equal,
                        tenantId,
                        currentTenantId,
                        boolParam.Type,
                        boolParam.TypeMapping);
                }

                // empty where sql
                return new SqlConstantExpression(true, typeof(bool), boolParam.TypeMapping);
            });

        return modelBuilder;
    }
}
