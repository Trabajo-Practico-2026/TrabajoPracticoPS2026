using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoPS.Domain.Exceptions;


namespace TrabajoPracticoPS.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);

            }
            catch (DbUpdateConcurrencyException)
            {
                // Aquí puedes manejar la excepción de concurrencia, por ejemplo, registrándola o lanzando una excepción personalizada
                throw new ConcurrencyException("Ocurrió un conflicto de concurrencia al intentar guardar los cambios.");
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("IX_RESERVATION_SeatId") == true)
            {
                // Captura el duplicate key del índice único
                throw new ConcurrencyException("La butaca ya fue reservada por otro usuario.");
            }
        }
    }
}
