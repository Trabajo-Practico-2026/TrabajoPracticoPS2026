using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Domain.Entities;
using TrabajoPracticoPS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TrabajoPracticoPS.Infrastructure.Persistence.Repositories
{
    public class ReservationRepository : IReservationRespository
    {
        readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateReservation(Reservation reservation)
        {
            await _context.AddAsync(reservation);
        }
        public async Task<Reservation?> GetReservationById(Guid reservationId)
        {
            return await _context.Reservations
                .Include(r => r.Seat)
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }

        public async Task<int> ReleaseExpiredReservationsAsync(CancellationToken ct)
        {
            var toExpire = await _context.Reservations
                .Where(r => r.Status == "Pending" && r.ExpiresAt < DateTime.UtcNow)
                .ToListAsync(ct);

            foreach (var res in toExpire)
            {
                res.Status = "Expired";
                _context.Seats.Where(s => s.Id == res.SeatId).ExecuteUpdate(s => s.SetProperty(seat => seat.Status, "Available"));

                var log = new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = res.UserId,
                    Action = "RESERVE_EXPIRED",
                    EntityType = "Seat",
                    EntityId = res.SeatId.ToString(),
                    Details = $"Reservation {res.Id} automatically expired for user {res.UserId}",
                    CreatedAt = DateTime.UtcNow
                };
                _context.AuditLogs.Add(log);
            }

            return await _context.SaveChangesAsync(ct);
        }
    }
}
