using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoPS.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        // Navegación
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<AuditLog>? AuditLogs { get; set; }
    }
}
