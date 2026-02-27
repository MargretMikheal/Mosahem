using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using mosahem.Application.Common;
using System.Net;

namespace Mosahem.Presentation.Filters
{
    public class ValidateModelIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var responseModel = new Response<string>()
                {
                    Succeeded = false,
                    Message = null,
                    Data = null,
                    Errors = new Dictionary<string, List<string>>(),
                    StatusCode = HttpStatusCode.BadRequest
                };

                context.HttpContext.Response.ContentType = "application/json";
                var IsIdProblem = context.ModelState.Where(m => m.Key.Contains("id", StringComparison.OrdinalIgnoreCase)).Any();

                if (IsIdProblem)
                    responseModel.Errors.Add("Id", new List<string> { "Invalid value" });
                context.Result = new BadRequestObjectResult(responseModel);
            }
        }
    }
}


