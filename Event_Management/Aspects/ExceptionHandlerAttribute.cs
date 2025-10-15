using Event_Management.Exceptions;
using EventFeedback.Exceptions;
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

            if (exceptionType == typeof(EventsNotFoundException))
            {
                var result = new NotFoundObjectResult(message);
                context.Result = result;
            }
            else if (exceptionType == typeof(EventAlreadyExistsException))
            {
                var result = new ConflictObjectResult(message);
               
                context.Result = result;
            }
            else if (exceptionType == typeof(EventCreationException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }
            else if (exceptionType == typeof(EventUpdateException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }
            else if(exceptionType == typeof(EventDeletionException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }

            else if (exceptionType == typeof(TicketCreationException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }
            else if (exceptionType == typeof(TicketAlreadyExistsException))
            {
                var result = new ConflictObjectResult(message);

                context.Result = result;
            }
            else if (exceptionType == typeof(TicketUpdateException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }
            else if (exceptionType == typeof(TicketDeletionException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }
            else if (exceptionType == typeof(TicketNotFoundException))
            {
                var result = new NotFoundObjectResult(message);
                context.Result = result;
            }
            else if (exceptionType == typeof(CategoryCreationException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }
            else if (exceptionType == typeof(CategoryAlreadyExistsException))
            {
                var result = new ConflictObjectResult(message);

                context.Result = result;
            }
            else if (exceptionType == typeof(CategoryUpdateException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }
            else if (exceptionType == typeof(CategoryDeletionException))
            {
                context.Result = new BadRequestObjectResult(new { error = message });
            }
            else if (exceptionType == typeof(CategoryNotFoundException))
            {
                var result = new NotFoundObjectResult(message);
                context.Result = result;
            }
            else if (exceptionType == typeof(FeedbackAlreadyExists))
            {
                var result = new ConflictObjectResult(message);
                context.Result = result;
            }
            else if (exceptionType == typeof(FeedbackNotFound))
            {
                var result = new NotFoundObjectResult(message);
                context.Result = result;
            }
            else if (exceptionType == typeof(InvalidSortFieldException))
            {
                var result = new NotFoundObjectResult(message);
                context.Result = result;
            }
            else if (exceptionType == typeof(ReplyAlreadyExists))
            {
                var result = new ConflictObjectResult(message);
                context.Result = result;
            }
            else if (exceptionType == typeof(FeedbackAlreadyArchivedException))
            {
                var result = new ConflictObjectResult(message);
                context.Result = result;
            }
            else if (exceptionType == typeof(FeedbackNotArchivedException))
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
