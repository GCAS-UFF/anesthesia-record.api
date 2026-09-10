namespace UFF.FichaAnestesica.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Requisição inválida em {Path}: {Message}", context.Request.Path, ex.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado em {Path}: {Message}", context.Request.Path, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = $"Erro interno no servidor - {ex.Message}"
                });
            }
        }
    }
}
