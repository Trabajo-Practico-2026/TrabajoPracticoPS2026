using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TrabajoPracticoPS.Application.DTOs;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Application.UseCases.Sector.Queries;
using TrabajoPracticoPS.Domain.Exceptions;

namespace TrabajoPracticoPS.Application.UseCases.Sector.Handlers
{
    public class GetAllSectorsQueryHandler : IRequestHandler<GetAllSectorsQuery, IEnumerable<SectorResponseDto>>
    {
        private readonly ISectorRepository _repository;
        public GetAllSectorsQueryHandler(ISectorRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SectorResponseDto>> Handle(GetAllSectorsQuery request, CancellationToken cancellationToken)
        {
            var sectors = await _repository.GetAllSector();
            if (sectors == null) throw new NotFoundException("No se encontraron sectores");
            return sectors.Select(s => new SectorResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Capacity = s.Capacity
            });
        }
    }
}
