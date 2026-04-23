using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using TrabajoPracticoPS.Application.UseCases.Reservation.Commands;
using TrabajoPracticoPS.Domain.Exceptions;

namespace TrabajoPracticoPS.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("reservations")]
        public async Task<IActionResult> ReserveSeat([FromBody] ReserveSeatCommand request)
        {
            try
            {
                await _mediator.Send(new ReserveSeatCommand(request.SeatId, request.UserId));
                return Ok(new { Message = "Butaca reservada exitosamente. Tienes 10 minutos para completar la compra." });
            }
            catch (DomainException ex)
            {
                return BadRequest(new {ex.Message });
            }
        }

    }
}
