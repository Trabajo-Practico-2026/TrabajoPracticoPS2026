using Microsoft.EntityFrameworkCore;
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
    public class SectorRepository : ISectorRepository
    {
        private readonly AppDbContext _context;
        public SectorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sector>> GetAllSector()
        {
            return await _context.Sectors.ToListAsync();
        }
    }
}
