using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoPS.Application.UseCases.Reservation.Commands
{
    public record ReserveSeatCommand(Guid SeatId, int UserId): IRequest
    {
    }
}
