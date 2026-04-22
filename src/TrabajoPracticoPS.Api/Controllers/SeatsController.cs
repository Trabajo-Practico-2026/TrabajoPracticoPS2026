using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoPS.Application.UseCases.Seat.Handlers;
using TrabajoPracticoPS.Application.UseCases.Seat.Queries;
using TrabajoPracticoPS.Application.UseCases.Sector.Queries;
using TrabajoPracticoPS.Domain.Entities;
using TrabajoPracticoPS.Infrastructure.Data;

namespace TrabajoPracticoPS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SeatsController: ControllerBase
    {
        private readonly IMediator _mediator;
        public SeatsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // Listar asientos de un sector
        [HttpGet("sector/{sectorId}")]
        public async Task<IActionResult> GetSeatsBySector(int sectorId)
        {
            var result = await _mediator.Send(new GetSeatsBySectorQuery(sectorId));
            return Ok(result);
        }
        
    }
}
