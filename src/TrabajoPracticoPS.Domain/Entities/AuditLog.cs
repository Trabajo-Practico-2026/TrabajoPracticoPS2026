using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoPS.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public int? UserId { get; set; }// Puede ser nulo si es un proceso del sistema
        public string Action { get; set; } = string.Empty; //RESERVE_ATTEMPT(Intento de reserva), RESERVE_SUCCESS (Reserva exitosa), EXPIRED (Expirado)
        public string EntityType {  get; set; } = string.Empty;// Reservation, Seat
        public string EntityId { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;// JSON con metadatos
        public DateTime CreatedAt { get; set; }

        //relacion
        public User? User { get; set; }
    }
}
