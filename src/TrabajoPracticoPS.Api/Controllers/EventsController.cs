using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoPS.Infrastructure.Persistence;
using TrabajoPracticoPS.Domain.Entities;

namespace TrabajoPracticoPS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventsController: ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Sectors)
                .ToListAsync();

            return Ok(events);
        }
    }
}
