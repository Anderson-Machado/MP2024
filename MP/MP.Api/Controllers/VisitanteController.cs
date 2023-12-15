using Microsoft.AspNetCore.Mvc;
using MP.Api.Controllers.Common;
using MP.Application.Models.App;
using MP.Application.Models.Pessoa;
using MP.Application.Services;
using MP.Application.Services.Interfaces;
using MP.CrossCutting.Utils.Resources;
using System.Net;
using static Microsoft.AspNetCore.Http.StatusCodes;
namespace MP.Api.Controllers
{

    /// <summary>
    /// API responsible for managing example.
    /// </summary>
    [Route("Visitante")]
    [ApiController]
    public class VisitanteController: ApiControllerBase
    {
        private readonly IVisitanteService _visitanteService;

        public VisitanteController(IVisitanteService visitanteService)
        {
            _visitanteService = visitanteService;
        }

        /// <summary>
        /// Get a example2 by example2Id.
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(PessoaModel), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> Get( [FromBody] AppRequest app)
        {
            var serviceResult = await _visitanteService.GetVisitanteByMatricula(app);

            if (serviceResult.ResultType == Application.Models.Common.ServiceResultTypes.Error)
            {
                return ErrorResponse(serviceResult.Notifications.Select(x => x.Message).First());
            }

            return SuccessResponse(serviceResult.Value, HttpStatusCode.OK);
        }
    }
}
