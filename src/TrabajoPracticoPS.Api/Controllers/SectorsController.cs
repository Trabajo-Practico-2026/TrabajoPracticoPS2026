using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoPS.Application.UseCases.Sector.Queries;

namespace TrabajoPracticoPS.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SectorsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("sectors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSectorsQuery());
            return Ok(result);
        }
        [HttpGet("events/{id}/sectors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllByEvent(int id)
        {
            var result = await _mediator.Send(new GetAllSectorsByEventQuery(id));
            return Ok(result);
        }
    }

}
