using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TrabajoPracticoPS.Application.UseCases.Reservation.Commands
{
    public record ConfirmPaymentCommand(Guid ReservationId) : IRequest;
    
}
