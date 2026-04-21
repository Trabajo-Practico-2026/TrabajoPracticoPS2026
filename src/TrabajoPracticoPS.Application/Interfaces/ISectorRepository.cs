using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Domain.Entities;

namespace TrabajoPracticoPS.Application.Interfaces
{
    public interface ISectorRepository
    {
       Task <IEnumerable<Sector>> GetAllSector();
    }
}
