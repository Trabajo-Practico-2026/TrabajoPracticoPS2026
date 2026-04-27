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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReserveSeat([FromBody] ReserveSeatCommand request)
        {
            try
            {
                await _mediator.Send(new ReserveSeatCommand(request.SeatId, request.UserId));
                return StatusCode(StatusCodes.Status201Created,new { Message = "Butaca reservada exitosamente. Tienes 10 minutos para completar la compra." });
            }
            catch (DomainException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound,new {ex.Message });
            }
        }

    }
}
