using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoPS.Domain.Entities
{
    public class Sector
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }

        // Navegación
        public Event? Event { get; set; }
        public ICollection<Seat>? Seats { get; set; }
    }
}
