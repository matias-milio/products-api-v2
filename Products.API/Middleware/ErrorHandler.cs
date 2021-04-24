using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Products.Application.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Products.API.Middleware
{
    public class ErrorHandler
    {
        private RequestDelegate Next;
        private ILogger<ErrorHandler> Logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            Next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(context, ex, Logger);                
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception ex, ILogger<ErrorHandler> logger)
        {
            object errors = null;
            switch (ex)
            {
                case CustomException me:
                    logger.LogError(ex, "Custom exception Error");
                    errors = me.Errors;
                    context.Response.StatusCode = (int)me.Code;
                break;
                case Exception e:
                    logger.LogError(ex, "Server Error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "No description" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";

            if (errors != null)
            {
                var res = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(res);
            }
        }

    }
}
