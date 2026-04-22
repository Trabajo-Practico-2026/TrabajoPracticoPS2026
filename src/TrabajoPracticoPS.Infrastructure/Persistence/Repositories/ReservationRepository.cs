using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Domain.Entities;
using TrabajoPracticoPS.Infrastructure.Data;

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
    }
}
