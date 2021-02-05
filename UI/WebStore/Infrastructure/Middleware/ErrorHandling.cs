using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class ErrorHandling
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ErrorHandling> _Logger;

        public ErrorHandling(RequestDelegate Next, ILogger<ErrorHandling> Logger)
        {
            _Next = Next;
            _Logger = Logger;
        }

        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                await _Next(Context);
            }
            catch(Exception e)
            {
                HandleException(e, Context);
                throw;
            }
        }

        private void HandleException(Exception error, HttpContext context)
        {
            _Logger.LogError(error, "Ошибка при обработке запроса к {0}", context.Request.Path);
        }
    }
}
