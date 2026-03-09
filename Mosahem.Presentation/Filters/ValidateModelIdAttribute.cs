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
                var idErrors = context.ModelState
                    .Where(m => m.Key.Contains("id", StringComparison.OrdinalIgnoreCase)
                             && m.Value.Errors.Count > 0);

                if (idErrors.Any())
                {
                    var responseModel = new Response<string>()
                    {
                        Succeeded = false,
                        Message = null,
                        Data = null,
                        Errors = new Dictionary<string, List<string>>
                {
                    { "Id", new List<string> { "Invalid value" } }
                },
                        StatusCode = HttpStatusCode.BadRequest
                    };

                    context.Result = new BadRequestObjectResult(responseModel);
                }
            }
        }
    }
}