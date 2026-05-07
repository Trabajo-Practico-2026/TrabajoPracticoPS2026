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
                var now = DateTime.UtcNow;
                var seat = await _seatRepository.GetSeatById(request.SeatId);

                if (seat == null) throw new NotFoundException("La butaca no existe.");

                bool isTrulyOccupied = seat.Status == "Sold" ||
                               (seat.Status == "Reserved" &&
                                seat.Reservation != null &&
                                seat.Reservation.ExpiresAt > now);

                if (isTrulyOccupied)
                    throw new ConflictException("La butaca ya no está disponible.");

                

                if(seat.Reservation == null)
                {
                    var reservation = new Domain.Entities.Reservation
                    {
                        Id = Guid.NewGuid(),
                        UserId = request.UserId,
                        SeatId = seat.Id,
                        Status = "Pending",
                        ReservedAt = now,
                        ExpiresAt = now.AddMinutes(5) // Tiempo de gracia
                    };
                    await _reservationRepository.CreateReservation(reservation);
                }
                else
                {
                    seat.Reservation.UserId = request.UserId;
                    seat.Reservation.Status = "Pending";
                    seat.Reservation.ReservedAt = now;
                    seat.Reservation.ExpiresAt = now.AddMinutes(5); // Tiempo de gracia
                }

                seat.Status = "Reserved";
                seat.Version++; // Incrementamos la versión para control de concurrencia

                var log = new AuditLog
                {
                    UserId = request.UserId,
                    Action = "RESERVE_ATTEMPT",
                    EntityType = "Seat",
                    EntityId = seat.Id.ToString(),
                    Details = $"User {request.UserId} attempted to reserve seat {seat.Id}",
                    CreatedAt = DateTime.UtcNow
                };

                
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
