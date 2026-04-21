using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoPS.Domain.Entities
{
    public class Seat
    {
        public Guid Id { get; set; }
        public int SectorId { get; set; }
        public string? RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public required string Status { get; set; } // Available, Reserved, Sold
        public int Version { get; set; }   // Para Optimistic Locking

        // Navegación
        public Sector? Sector { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
