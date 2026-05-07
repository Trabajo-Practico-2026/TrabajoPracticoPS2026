using Microsoft.EntityFrameworkCore;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Domain.Entities;
using TrabajoPracticoPS.Infrastructure.Data;


namespace TrabajoPracticoPS.Infrastructure.Persistence.Repositories
{
    public class SeatRepository: ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetAllSeats()
        {
            return await _context.Seats.ToListAsync();
        }

        public async Task<Seat> GetSeatById(Guid seatId)
        {
            return await _context.Seats
                .Include(s => s.Reservation)
                .FirstOrDefaultAsync(s => s.Id == seatId);
        }

        public async Task<IEnumerable<Seat>> GetSeatsBySector(int sectorId)
        {
            var now = DateTime.UtcNow;

            var seats = await _context.Seats
                .Include(s => s.Reservation)
                .Where(s => s.SectorId == sectorId)
                .ToListAsync();

            foreach (var seat in seats)
            {
                // Lógica de "Liberación Instantánea"
                if (seat.Reservation != null)
                {
                    if (seat.Reservation.Status == "Paid")
                    {
                        seat.Status = "Sold";
                    }
                    else if (seat.Reservation.Status == "Pending" && seat.Reservation.ExpiresAt > now)
                    {
                        seat.Status = "Reserved";
                    }
                    else
                    {
                        // Si está pendiente pero ya expiró (aunque el worker no haya pasado)
                        seat.Status = "Available";
                    }
                }
                else
                {
                    seat.Status = "Available";
                }
            }

            return seats;
        }
    }
}
