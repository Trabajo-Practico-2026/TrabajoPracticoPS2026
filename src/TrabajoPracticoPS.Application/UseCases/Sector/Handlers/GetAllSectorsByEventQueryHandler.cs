using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Application.DTOs;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Application.UseCases.Sector.Queries;

namespace TrabajoPracticoPS.Application.UseCases.Sector.Handlers
{
    public class GetAllSectorsByEventQueryHandler : IRequestHandler<GetAllSectorsByEventQuery, IEnumerable<SectorResponseDto>>
    {
        private readonly ISectorRepository _repository;
        public GetAllSectorsByEventQueryHandler(ISectorRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SectorResponseDto>> Handle(GetAllSectorsByEventQuery request, CancellationToken cancellationToken)
        {
            var sectors = await _repository.GetAllSectorsByEvent(request.EventId);
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
