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
    }
}
