using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CME.Common.Filters
{
    public class SwaggerHeadersFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.TenantId,
                In = ParameterLocation.Header,
                Required = true,
                Example = new OpenApiString("cme")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.TimeZone,
                In = ParameterLocation.Header,
                Required = false,
                Example = new OpenApiString("Asia/Beirut")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.OsName,
                Description = "Example: ANDROID, OS",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema()
                {
                    Type = "string",
                    Nullable = true
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.AppVersion,
                Description = "Example: 1.0.0",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema()
                {
                    Type = "string",
                    Nullable = true
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.AcceptLanguage,
                Description = "Example: ar",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema()
                {
                    Type = "string",
                    Nullable = true
                }
            });
        }
    }
}
