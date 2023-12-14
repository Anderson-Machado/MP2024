using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using MP.CrossCutting.ProblemDetail;
using MP.CrossCutting.Utils.Resources;
using System.Net;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace MP.Api.Controllers.Common
{
    [ApiController]
    [Produces(ResponseMediaTypes.APPLICATON_JSON, ResponseMediaTypes.APPLICATON_PROBLEM_JSON)]
    [ProducesResponseType(Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), Status401Unauthorized)]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult SuccessResponse(object? value, HttpStatusCode httpStatusCode)
        {
            Response.StatusCode = (int)httpStatusCode;
            return new JsonResult(value);
        }

        protected IActionResult UnprocessableResponse(string message)
        {
            Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            return new JsonResult(CreateProblemDetails(HttpStatusCode.UnprocessableEntity, message));
        }

        protected IActionResult ErrorResponse(IReadOnlyCollection<Notification> notifications)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult(CreateProblemDetails(notifications));
        }

        protected IActionResult NotFoundResponse(string message)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new JsonResult(CreateProblemDetails(HttpStatusCode.NotFound, message));
        }

        private ProblemDetails CreateProblemDetails(IReadOnlyCollection<Notification> notifications)
        {
            return CustomProblemDetailsFactory.CreateCustomValidationProblemDetails(HttpContext, notifications);
        }

        private ProblemDetails CreateProblemDetails(HttpStatusCode statusCode, string detail)
        {
            return CustomProblemDetailsFactory.Create(HttpContext, (int)statusCode, Messages.DefaultProblemDetailsTitle, detail);
        }
    }
}