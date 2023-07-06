using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace scada_back.Exception.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                ObjectNameTakenException => new ConflictObjectResult(context.Exception.Message),
                ObjectNotFoundException => new NotFoundObjectResult(context.Exception.Message),
                ActionNotExecutedException => new ObjectResult(context.Exception.Message) { StatusCode = 503 },
                _ => new BadRequestObjectResult(context.Exception.Message)
            };
        }
    }
}