using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Application.UseCases.Reservation.Commands;
using TrabajoPracticoPS.Domain.Entities;
using TrabajoPracticoPS.Domain.Exceptions;

namespace TrabajoPracticoPS.Application.UseCases.Reservation.Handlers
{
    public class CancelReservedSeatCommandHandler : IRequestHandler<CancelReservedSeatCommand>
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IReservationRespository _reservationRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        public CancelReservedSeatCommandHandler(ISeatRepository seatRepository, IReservationRespository reservationRepository, IAuditLogRepository auditLogRepository)
        {
            _seatRepository = seatRepository;
            _reservationRepository = reservationRepository;
            _auditLogRepository = auditLogRepository;
        }
        public async Task Handle(CancelReservedSeatCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetReservationById(request.ReservationId);
            if (reservation == null) throw new NotFoundException("La Reserva no existe.");
            var seat = await _seatRepository.GetSeatById(reservation.SeatId);
            seat.Status = "Available";
            await _reservationRepository.CancelReservation(reservation);
            var log = new AuditLog
            {
                UserId = reservation.UserId,
                Action = "EXPIRED",
                EntityType = "Seat",
                EntityId = reservation.SeatId.ToString(),
                Details = $"User {reservation.UserId} attempted to reserve seat {reservation.SeatId}",
                CreatedAt = DateTime.UtcNow
            };
            await _auditLogRepository.CreateAuditLog(log);
            await _seatRepository.UpdateSeat(seat);

        }
    }
}
