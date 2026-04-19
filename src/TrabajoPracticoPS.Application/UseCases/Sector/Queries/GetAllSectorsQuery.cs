using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TrabajoPracticoPS.Application.DTOs;

namespace TrabajoPracticoPS.Application.UseCases.Sector.Queries
{
    public record GetAllSectorsQuery() : IRequest<IEnumerable<SectorResponseDto>>
    {
    }
}
