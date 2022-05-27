
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace PersonAPI.Services
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILoggerManager _logger;
        public UnhandledExceptionFilterAttribute(ILoggerManager logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            // Customize this object to fit your needs
            ObjectResult result;
            if (context.Exception.InnerException == null)
            {
                _logger.LogError(context.Exception.Message + "; " + context.Exception.Source);
                result = new ObjectResult(new
                {
                    context.Exception.Message,
                    context.Exception.Source,
                    ExceptionType = context.Exception.GetType().FullName,
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
            else
            {
                _logger.LogError(context.Exception.InnerException.Message + "; " + context.Exception.InnerException.Source);
                result = new ObjectResult(new
                {
                    context.Exception.InnerException.Message,
                    context.Exception.InnerException.Source,
                    ExceptionType = context.Exception.InnerException.GetType().FullName,
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            // Set the result
            context.Result = result;
        }
    }

}
