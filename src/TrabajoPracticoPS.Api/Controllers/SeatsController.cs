using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoPS.Infrastructure.Data;
using TrabajoPracticoPS.Domain.Entities;


namespace TrabajoPracticoPS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SeatsController: ControllerBase
    {
        private readonly AppDbContext _context;

        public SeatsController(AppDbContext context)
        {
            _context = context;
        }

        // Listar asientos de un sector
        [HttpGet("sector/{sectorId}")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeatsBySector(int sectorId)
        {
            var seats = await _context.Seats
                .Where(s => s.SectorId == sectorId)
                .ToListAsync();

            return Ok(seats);
        }

        // Intentar reservar un asiento
        [HttpPost("{seatId}/reserve")]
        public async Task<ActionResult> ReserveSeat(Guid seatId, int userId)
        {
            var seat = await _context.Seats.FindAsync(seatId);

            if (seat == null)
                return NotFound("El asiento no existe.");

            if (seat.Status != "Available")
                return Conflict("El asiento ya está reservado o vendido.");

            // Cambiar estado del asiento
            seat.Status = "Reserved";
            seat.Version++;

            // Crear reserva
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SeatId = seatId,
                Status = "Pending",
                ReservedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(5)
            };
            _context.Reservations.Add(reservation);

            // Registrar en AuditLog
            var audit = new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Action = "RESERVE_SUCCESS",
                EntityType = "Reservation",
                EntityId = reservation.Id.ToString(),
                Details = $"{{ \"seatId\": \"{seatId}\" }}",
                CreatedAt = DateTime.Now
            };
            _context.AuditLogs.Add(audit);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Reserva exitosa", reservationId = reservation.Id });
        }
    }
}
