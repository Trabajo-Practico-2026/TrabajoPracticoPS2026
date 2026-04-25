using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoPS.Infrastructure.Persistence;
using TrabajoPracticoPS.Domain.Entities;
using MediatR;
using TrabajoPracticoPS.Application.UseCases.Event.Queries;

namespace TrabajoPracticoPS.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class EventsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var result = await _mediator.Send(new GetAllEventsQuery());

            return Ok(result);
        }
    }
}
