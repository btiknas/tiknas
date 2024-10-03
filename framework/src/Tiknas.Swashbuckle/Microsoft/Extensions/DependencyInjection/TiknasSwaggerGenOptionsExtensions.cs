using System;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tiknas.Swashbuckle;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasSwaggerGenOptionsExtensions
{
    public static void HideTiknasEndpoints(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.DocumentFilter<TiknasSwashbuckleDocumentFilter>();
    }

    public static void UserFriendlyEnums(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.SchemaFilter<TiknasSwashbuckleEnumSchemaFilter>();
    }

    public static void CustomTiknasSchemaIds(this SwaggerGenOptions options)
    {
        string SchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType)
            {
                return modelType.FullName!.Replace("[]", "Array");
            }

            var prefix = modelType.GetGenericArguments()
                .Select(SchemaIdSelector)
                .Aggregate((previous, current) => previous + current);
            return modelType.FullName!.Split('`').First() + "Of" + prefix;
        }

        options.CustomSchemaIds(SchemaIdSelector);
    }
}
