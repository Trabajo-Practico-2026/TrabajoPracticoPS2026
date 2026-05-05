using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Application.DTOs;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Application.UseCases.Seat.Queries;
using TrabajoPracticoPS.Domain.Exceptions;

namespace TrabajoPracticoPS.Application.UseCases.Seat.Handlers
{
    public class GetSeatsBySectorQueryHandler : IRequestHandler<GetSeatsBySectorQuery, IEnumerable<SeatResponseDto>>
    {
        private readonly ISeatRepository _repository;

        public GetSeatsBySectorQueryHandler(ISeatRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<SeatResponseDto>> Handle(GetSeatsBySectorQuery request, CancellationToken cancellationToken)
        {
            var seats = await _repository.GetSeatsBySector(request.sectorId);
            if (seats == null) throw new NotFoundException($"No se encontraron asientos para el sector con ID {request.sectorId}.");
            return seats.Select(s => new SeatResponseDto
            {
                Id = s.Id,
                SectorId = s.SectorId,
                RowIdentifier = s.RowIdentifier,
                SeatNumber = s.SeatNumber,
                Status = s.Status
            });
        }
    }
}
