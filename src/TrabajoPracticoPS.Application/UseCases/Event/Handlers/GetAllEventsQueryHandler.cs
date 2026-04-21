using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TrabajoPracticoPS.Application.DTOs;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Application.UseCases.Event.Queries;

namespace TrabajoPracticoPS.Application.UseCases.Event.Handlers
{
    public class GetAllEventsQueryHandler: IRequestHandler<GetAllEventsQuery , IEnumerable<EventResponseDto>>
    {
        private readonly IEventRepository _repository;

        public GetAllEventsQueryHandler(IEventRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<EventResponseDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _repository.GetAllEvents();
            return events.Select(e => new EventResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                EventDate = e.EventDate,
                Venue = e.Venue,
                Status = e.Status
            });
        }
    }
    
}
