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
        [HttpPost("reservations/{id}/confirm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ConfirmPayment(Guid id)
        {
                await _mediator.Send(new ConfirmPaymentCommand(id));
                return Ok(new { Message = "Pago confirmado. La butaca fue marcada como vendida." });
            
        }

    }
}
