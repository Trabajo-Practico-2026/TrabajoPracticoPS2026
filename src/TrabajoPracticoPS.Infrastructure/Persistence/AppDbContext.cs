using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Domain.Entities;

namespace TrabajoPracticoPS.Infrastructure.Persistence

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //tablas
        public DbSet<Event> Events { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }


        //relaciones

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sector>()
               .HasOne(s => s.Event)
               .WithMany(e => e.Sectors)
               .HasForeignKey(s => s.EventId);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Sector)
                .WithMany(s => s.Seats)
                .HasForeignKey(s => s.SectorId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Seat)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SeatId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            //Seeding inicial (evento, sectores y asientos)
            var eventId = 1;
            var sector1Id = 1;
            var sector2Id = 2;

            modelBuilder.Entity<Event>().HasData(new Event
            {
                Id = eventId,
                Name = "Concierto de Rock",
                EventDate = DateTime.Now.AddMonths(1),
                Venue = "Estadio Quilmes",
                Status = "Active"
            });

            modelBuilder.Entity<Sector>().HasData(
                new Sector { Id = sector1Id, EventId = eventId, Name = "Platea", Price = 5000, Capacity = 50 },
                new Sector { Id = sector2Id, EventId = eventId, Name = "Campo", Price = 3000, Capacity = 50 }
                );

            var seats = new List<Seat>();
            for(int i =1; i <= 50; i++)
            {
                seats.Add(new Seat
                {
                    Id = Guid.NewGuid(),
                    SectorId = sector1Id,
                    RowIdentifier = "A",
                    SeatNumber = i,
                    Status = "Available",
                    Version = 1
                });

                seats.Add(new Seat
                {
                    Id = Guid.NewGuid(),
                    SectorId = sector2Id,
                    RowIdentifier = "B",
                    SeatNumber = i,
                    Status = "Available",
                    Version = 1
                });
            }
            modelBuilder.Entity<Seat>().HasData(seats);
        }
    }
}
