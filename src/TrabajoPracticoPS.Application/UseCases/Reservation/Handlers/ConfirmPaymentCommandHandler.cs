using MediatR;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Application.UseCases.Reservation.Commands;
using TrabajoPracticoPS.Domain.Entities;
using TrabajoPracticoPS.Domain.Exceptions;

namespace TrabajoPracticoPS.Application.UseCases.Reservation.Handlers
{
    public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand>
    {
        private readonly IReservationRespository _reservationRespository;
        private readonly ISeatRepository _seatRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmPaymentCommandHandler(
            IReservationRespository reservationRespository,
            ISeatRepository seatRepository,
            IAuditLogRepository auditLogRepository,
            IUnitOfWork unitOfWork )
        {
            _reservationRespository = reservationRespository;
            _seatRepository = seatRepository;
            _auditLogRepository = auditLogRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            //Buscar la reserva
            var reservation = await _reservationRespository.GetReservationById(request.ReservationId);
            if( reservation == null) 
                throw new NotFoundException("La reserva no existe.");

            //solo se puede pagar una reserva pendiente
            if (reservation.Status != "Pending")
                throw new ConflictException("La reserva no esta en estado pendiente.");

            //Vereficar que no haya expirado
            if (DateTime.UtcNow > reservation.ExpiresAt)
                throw new ConflictException("La reserva ha expirado y ya no puede ser confirmada");

            //buscar la butaca asociada
            var seat = await _seatRepository.GetSeatById(reservation.SeatId);
            if (seat == null)
                throw new NotFoundException("La butaca asociada no existe");

            //5. Operación atómica: los tres cambios se guardan en un solo SaveChangesAsync
            // Si cualquiera falla, el UnitOfWork hace rollback completo (ACID)
            reservation.Status = "Paid";
            seat.Status = "Sold";

            var log = new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = reservation.UserId,
                Action = "PAYMENT_CONFIRMED",
                EntityType = "Reservation",
                EntityId = reservation.Id.ToString(),
                Details = $"User {reservation.UserId} confirmed payment for reservation {reservation.Id}, seat{seat.Id}",
                CreatedAt = DateTime.UtcNow,
            };

            await _auditLogRepository.CreateAuditLog(log);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
