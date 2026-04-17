using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoPS.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public Guid SeatId { get; set; }
        public string Status { get; set; } = "Pending";// Pending(pendiente), Paid(pagado), Expired(vencido)
        public DateTime ReservedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        

        //relaciones
        public User User { get; set; } = null!;
        public Seat Seat { get; set; } = null!;

    }
}
