using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Domain.Entities;
using TrabajoPracticoPS.Infrastructure.Data;

namespace TrabajoPracticoPS.Infrastructure.Persistence
{
    public class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Seats.Any()) return;

            var seats = new List<Seat>();

            for (int i = 1; i <= 50; i++)
            {
                seats.Add(new Seat
                {
                    Id = Guid.NewGuid(),
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = i,
                    Status = "Available",
                    Version = 1
                });

                seats.Add(new Seat
                {
                    Id = Guid.NewGuid(),
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = i,
                    Status = "Available",
                    Version = 1
                });
            }

            context.Seats.AddRange(seats);
            context.SaveChanges();
        }
    }
}


