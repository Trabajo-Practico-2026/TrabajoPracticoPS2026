using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrabajoPracticoPS.Domain.Entities;

namespace TrabajoPracticoPS.Infrastructure.Persistence.EntityConfigurations
{
    public class EventConfiguration: IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);//Define la clave primaria de la tabla como la propiedad Id
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);//Configura la columna (Name)
            builder.Property(e => e.Venue).IsRequired().HasMaxLength(200);//Configura la columna Venue (lugar del evento)
            builder.Property(e => e.Status).IsRequired().HasMaxLength(50);//Configura la columna (Status)
            builder.HasMany(e => e.Sectors) //Un Event tiene muchos Sectors.
                   .WithOne(s => s.Event) //Cada Sector está asociado a un solo Event (WithOne).
                   .HasForeignKey(s => s.EventId);//La clave foránea en Sector es EventId
        }
    }
}
