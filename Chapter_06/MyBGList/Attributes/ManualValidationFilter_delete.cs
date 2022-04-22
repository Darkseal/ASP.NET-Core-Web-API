/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MyBGList.Attributes
{
    public class ManualValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(context.ModelState);
                details.Extensions["traceId"] =
                    Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
                if (context.ModelState.Keys.Any(k => k == "PageSize"))
                {
                    details.Type =
                        "https://tools.ietf.org/html/rfc4918#section-11.2";
                    details.Status = StatusCodes.Status422UnprocessableEntity;
                    context.Result = new UnprocessableEntityObjectResult(details);
                }
                else
                {
                    details.Type =
                        "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    details.Status = StatusCodes.Status400BadRequest;
                    context.Result = new BadRequestObjectResult(details);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) 
        { 
            // do nothing
        }
    }
}
*/