namespace Products.Api.Middleware
{
    public class MasterKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _masterKey;

        public MasterKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _masterKey = config["MasterKey"] ?? "MASTER123";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("x-master-key", out var key) ||
                key != _masterKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized: Invalid master key.");
                return;
            }

            await _next(context);
        }
    }

    public static class MasterKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMasterKey(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MasterKeyMiddleware>();
        }
    }
}
