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
    public class ReserveSeatCommandHandler : IRequestHandler<ReserveSeatCommand>
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IReservationRespository _reservationRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReserveSeatCommandHandler(ISeatRepository seatRepository,IReservationRespository reservationRepository, IAuditLogRepository auditLogRepository, IUnitOfWork unitOfWork)
        {
            _seatRepository = seatRepository;
            _reservationRepository = reservationRepository;
            _auditLogRepository = auditLogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReserveSeatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var seat = await _seatRepository.GetSeatById(request.SeatId);

                if (seat == null) throw new NotFoundException("La butaca no existe.");

                if (seat.Status != "Available") throw new ConflictException("La butaca ya no está disponible.");

                seat.Status = "Reserved";
                seat.Version++; // Incrementamos la versión para control de concurrencia


                var reservation = new Domain.Entities.Reservation
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    SeatId = seat.Id,
                    Status = "Pending",
                    ReservedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5) // Tiempo de gracia
                };


                var log = new AuditLog
                {
                    UserId = request.UserId,
                    Action = "RESERVE_ATTEMPT",
                    EntityType = "Seat",
                    EntityId = seat.Id.ToString(),
                    Details = $"User {request.UserId} attempted to reserve seat {seat.Id}",
                    CreatedAt = DateTime.UtcNow
                };


                await _reservationRepository.CreateReservation(reservation);
                await _auditLogRepository.CreateAuditLog(log);


                await _unitOfWork.SaveChangesAsync();
            }
            catch (DomainException)
            {
                throw;
            }
            catch (ConcurrencyException)
            { 
                throw new DomainException("La butaca ya no está disponible. Intente nuevamente.");
            }
        }
    }
}
