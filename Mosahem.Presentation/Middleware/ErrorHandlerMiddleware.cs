using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using mosahem.Application.Common;
using mosahem.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace mosahem.Presentation.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new Response<string>
                {
                    Succeeded = false,
                    Message = error?.Message,
                    Data = null,
                    Errors = new Dictionary<string, List<string>>(),
                    StatusCode = HttpStatusCode.InternalServerError
                };

                switch (error)
                {
                    case UnauthorizedAccessException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case ValidationExceptionWithErrors e:
                        responseModel.Message = e.Message;
                        responseModel.Errors = e.Errors;
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;

                    case FluentValidation.ValidationException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;

                    case KeyNotFoundException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case DbUpdateException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ArgumentNullException e:
                        responseModel.Message = $"Invalid Value. (Parameter '{e.ParamName}')";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        responseModel.Message = error.Message;
                        if (error.InnerException != null)
                            responseModel.Message += "\n" + error.InnerException.Message;

                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
