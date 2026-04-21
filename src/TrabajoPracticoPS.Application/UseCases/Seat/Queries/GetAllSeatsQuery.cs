using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TrabajoPracticoPS.Application.DTOs;

namespace TrabajoPracticoPS.Application.UseCases.Seat.Queries
{
    public class GetAllSeatsQuery() : IRequest<IEnumerable<SeatResponseDto>>
    {
       
    }
}
