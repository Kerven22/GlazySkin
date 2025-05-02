namespace GlazySkin.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

            }
            catch(Exception exception)
            {
                switch(exception)
                {
                    default: await httpContext.Response.WriteAsJsonAsync("Error");
                            break;

                }
                
            }
        }
    }
}
