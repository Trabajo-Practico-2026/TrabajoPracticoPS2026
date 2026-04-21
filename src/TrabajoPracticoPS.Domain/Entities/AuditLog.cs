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
        public int? UserId { get; set; }   // Nullable: puede ser el sistema
        public required string Action { get; set; }
        public required string EntityType { get; set; }
        public required string EntityId { get; set; }
        public required string Details { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navegación
        public User? User { get; set; }
    }
}
