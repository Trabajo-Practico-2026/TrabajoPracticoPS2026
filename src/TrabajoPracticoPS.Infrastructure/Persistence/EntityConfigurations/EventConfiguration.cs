using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrabajoPracticoPS.Domain.Entities;

namespace TrabajoPracticoPS.Infrastructure.Persistence.EntityConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Venue).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Status).IsRequired().HasMaxLength(50);

            builder.HasData(new Event
            {
                Id = 1,
                Name = "Concierto de Rock",
                EventDate = new DateTime(2026, 6, 1, 21, 0, 0),
                Venue = "Estadio Quilmes",
                Status = "Active"
            });
        }
    }
}