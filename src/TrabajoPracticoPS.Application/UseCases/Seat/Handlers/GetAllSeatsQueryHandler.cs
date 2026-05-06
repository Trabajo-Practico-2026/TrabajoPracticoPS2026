using MediatR;
using TrabajoPracticoPS.Application.DTOs;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Application.UseCases.Seat.Queries;
using TrabajoPracticoPS.Domain.Exceptions;

namespace TrabajoPracticoPS.Application.UseCases.Seat.Handlers
{
    public class GetAllSeatsQueryHandler : IRequestHandler<GetAllSeatsQuery, IEnumerable<SeatResponseDto>>
    {
        private readonly ISeatRepository _repository;

        public GetAllSeatsQueryHandler(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SeatResponseDto>> Handle(GetAllSeatsQuery request, CancellationToken cancellationToken)
        {
            var seats = await _repository.GetAllSeats();
            if (seats == null) throw new NotFoundException("No hay sillas.");
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
