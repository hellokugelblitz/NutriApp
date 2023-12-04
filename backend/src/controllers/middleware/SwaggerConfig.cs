using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NutriApp.Controllers.Middleware;

public class AddHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext ctx)
    {
        var requiresAuthorization =
            ctx.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
            || ctx.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        if (requiresAuthorization)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "sessionKey",
                In = ParameterLocation.Header,
                Description = "Session Key",
                Required = true
            });
        }
    }
}