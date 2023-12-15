using Microsoft.AspNetCore.Mvc;
using MP.Api.Controllers.Common;
using MP.Application.Models.Pessoa;
using MP.Application.Services.Interfaces;
using MP.CrossCutting.Utils.Resources;
using System.Net;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace MP.Api.Controllers
{

    /// <summary>
    /// API responsible for managing example.
    /// </summary>
    [Route("Pessoa")]
    [ApiController]
    public class PessoaController : ApiControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        /// <summary>
        /// Get a example2 by example2Id.
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        [HttpGet("{matricula}")]
        [ProducesResponseType(typeof(PessoaModel), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] decimal matricula)
        {
            var serviceResult = await _pessoaService.GetPessoaByMatricula(matricula);

            if (serviceResult.ResultType == Application.Models.Common.ServiceResultTypes.NotFound)
            {
                return NotFoundResponse(Messages.NotFound);
            }

            return SuccessResponse(serviceResult.Value, HttpStatusCode.OK);
        }

    }
}
