using Microsoft.AspNetCore.Mvc;
using MP.Api.Controllers.Common;
using MP.Application.Models.Example;
using MP.Application.Services.Interfaces;
using MP.CrossCutting.Utils.Resources;
using System.Net;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace MP.Api.Controllers
{
    /// <summary>
    /// API responsible for managing example.
    /// </summary>
    [Route("example")]
    [ApiController]
    public class ExampleController : ApiControllerBase
    {
        private readonly IExampleService _service;

        /// <summary>
        /// Ctor with DI
        /// </summary>
        /// <param name="service"></param>
        public ExampleController(IExampleService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a example.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ExampleModel), Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ExamplePostModel model)
        {
            var serviceResult = await _service.Create(model);

            if (!serviceResult.IsValid)
            {
                return ErrorResponse(serviceResult.Notifications);
            }

            return SuccessResponse(serviceResult.Value, HttpStatusCode.Created);
        }

        /// <summary>
        /// Updating a example.
        /// </summary>
        /// <param name="exampleId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{exampleId}")]
        [ProducesResponseType(typeof(ExampleModel), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> Patch([FromRoute] Guid? exampleId, [FromBody] ExamplePatchModel model)
        {
            model.ExampleId = exampleId;
            var serviceResult = await _service.Update(model);

            if (!serviceResult.IsValid)
            {
                return ErrorResponse(serviceResult.Notifications);
            }

            if (serviceResult.ResultType == Application.Models.Common.ServiceResultTypes.NotFound)
            {
                return NotFoundResponse(Messages.NotFound);
            }

            return SuccessResponse(serviceResult.Value, HttpStatusCode.OK);
        }

        /// <summary>
        /// Deleting a example.
        /// </summary>
        /// <param name="exampleId"></param>
        /// <returns></returns>
        [HttpDelete("{exampleId}")]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid? exampleId)
        {
            var serviceResult = await _service.Delete(exampleId);

            if (!serviceResult.IsValid)
            {
                return ErrorResponse(serviceResult.Notifications);
            }

            if (serviceResult.ResultType == Application.Models.Common.ServiceResultTypes.NotFound)
            {
                return NotFoundResponse(Messages.NotFound);
            }

            return NoContent();
        }

        /// <summary>
        /// Get a example by exampleId.
        /// </summary>
        /// <param name="exampleId"></param>
        /// <returns></returns>
        [HttpGet("{exampleId}")]
        [ProducesResponseType(typeof(ExampleModel), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid? exampleId)
        {
            var serviceResult = await _service.Get(exampleId);

            if (!serviceResult.IsValid)
            {
                return ErrorResponse(serviceResult.Notifications);
            }

            if (serviceResult.ResultType == Application.Models.Common.ServiceResultTypes.NotFound)
            {
                return NotFoundResponse(Messages.NotFound);
            }

            return SuccessResponse(serviceResult.Value, HttpStatusCode.OK);
        }

        /// <summary>
        /// Get a examples 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExampleModel>), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var serviceResultList = await _service.List();
            return SuccessResponse(serviceResultList.Value, HttpStatusCode.OK);
        }
    }
}