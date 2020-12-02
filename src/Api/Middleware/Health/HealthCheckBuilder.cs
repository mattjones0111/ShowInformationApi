using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api.Middleware.Health
{
    public static class HealthCheckBuilder
    {
        public static IApplicationBuilder UseApiHealthChecks(
            this IApplicationBuilder builder)
        {
            return builder.Map(
                "/health",
                config => config.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"result\" : \"ok\"}");
                }));
        }
    }
}
