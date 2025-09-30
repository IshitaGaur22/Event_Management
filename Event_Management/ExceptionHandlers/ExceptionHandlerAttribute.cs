using Event_Management.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Event_Management.ExceptionHandlers
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var message = context.Exception.Message;

            if (exceptionType == typeof(EventNotFoundException))
            {
                var result = new NotFoundObjectResult(message);
                context.Result = result;
            }
            else
            if (exceptionType == typeof(EventAlreadyExistsException))
            {
                var result = new ConflictObjectResult(message);
                context.Result = result;
            }
            else
            {
                var result = new StatusCodeResult(500);
                context.Result = result;
            }
        }
    }
}
