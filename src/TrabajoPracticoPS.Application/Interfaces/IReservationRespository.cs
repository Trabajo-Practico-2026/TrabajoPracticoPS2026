using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Domain.Entities;

namespace TrabajoPracticoPS.Application.Interfaces
{
    public interface IReservationRespository
    {
        Task CreateReservation(Reservation reservation);
        //Para el pago transaccional
        Task<Reservation?> GetReservationById(Guid reservationId);
        Task<int> ReleaseExpiredReservationsAsync(CancellationToken ct);
    }
}
