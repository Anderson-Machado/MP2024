using Microsoft.AspNetCore.Mvc;
using MP.Api.Controllers.Common;
using MP.Application.Models.Common;
using MP.Application.Models.Example2;
using MP.Application.Services.Interfaces;
using MP.CrossCutting.Utils.Resources;
using System.Net;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace MP.Api.Controllers
{
    /// <summary>
    /// API responsible for managing example2.
    /// </summary>
    [Route("example2")]
    [ApiController]
    public class Example2Controller : ApiControllerBase
    {
        private readonly IExample2Service _service;

        /// <summary>
        /// Ctor with DI
        /// </summary>
        /// <param name="service"></param>
        public Example2Controller(IExample2Service service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a example2.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Example2Model), Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Example2PostModel model)
        {
            var serviceResult = await _service.Create(model);

            if (!serviceResult.IsValid)
            {
                return ErrorResponse(serviceResult.Notifications);
            }

            return SuccessResponse(serviceResult.Value, HttpStatusCode.Created);
        }

        /// <summary>
        /// Updating a example2.
        /// </summary>
        /// <param name="example2Id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{example2Id}")]
        [ProducesResponseType(typeof(Example2Model), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> Patch([FromRoute] Guid? example2Id, [FromBody] Example2PatchModel model)
        {
            model.Example2Id = example2Id;
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
        /// Deleting a example2.
        /// </summary>
        /// <param name="example2Id"></param>
        /// <returns></returns>
        [HttpDelete("{example2Id}")]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid? example2Id)
        {
            var serviceResult = await _service.Delete(example2Id);

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
        /// Get a example2 by example2Id.
        /// </summary>
        /// <param name="example2Id"></param>
        /// <returns></returns>
        [HttpGet("{example2Id}")]
        [ProducesResponseType(typeof(Example2Model), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid? example2Id)
        {
            var serviceResult = await _service.Get(example2Id);

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
        ///  Search a examples2
        /// </summary>
        /// <param name="term"></param>
        /// <param name="page" example="1"></param>
        /// <param name="pageSize" example="10"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ListExample2Model), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] string? term, [FromQuery] int? page, [FromQuery] int? pageSize)
        {
            SearchModel model = new() { Term = term, Page = page, PageSize = pageSize };

            var serviceResult = await _service.SearchMany(model);

            if (!serviceResult.IsValid)
            {
                return ErrorResponse(serviceResult.Notifications);
            }

            return SuccessResponse(serviceResult.Value, HttpStatusCode.OK);
        }
    }
}