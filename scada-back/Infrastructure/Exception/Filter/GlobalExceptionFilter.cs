using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace scada_back.Infrastructure.Exception.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            System.Exception exception = context.Exception;
            string errorMessage = exception.Message;
            _logger.LogError(exception, "An exception occurred: {ErrorMessage}", errorMessage);
            
            context.Result = context.Exception switch
            {
                ObjectNameTakenException => new ConflictObjectResult(context.Exception.Message),
                ObjectNotFoundException => new NotFoundObjectResult(context.Exception.Message),
                InvalidSignalTypeException => new BadRequestObjectResult(context.Exception.Message),
                ActionNotExecutedException => new ObjectResult(context.Exception.Message) { StatusCode = 503 },
                _ => new BadRequestObjectResult(context.Exception.Message)
            };
        }
    }
}