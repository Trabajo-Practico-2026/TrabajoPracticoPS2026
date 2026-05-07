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
        //Incluye la butaca para poder modificar su estado en la misma transaccion
        public async Task<Reservation?> GetReservationById (Guid reservationId)
        {
            return await _context.Reservations
                .Include(r=> r.Seat)
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }
    }
}
