using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TrainingManagement.Common;

namespace TrainingManagement.Filters
{
    public class ApiResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var response = new ApiResponse<object>
                {
                    Data = objectResult.Value
                };
                context.Result = new JsonResult(response);
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new ApiResponse());
            }
        }
    }
}