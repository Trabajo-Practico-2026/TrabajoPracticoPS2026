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
    public class SectorConfiguration : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.ToTable("Sectors");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(s => s.Event)
                .WithMany(e => e.Sectors)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Sector { Id = 1, EventId = 1, Name = "Platea", Price = 5000, Capacity = 50 },
                new Sector { Id = 2, EventId = 1, Name = "Campo", Price = 3000, Capacity = 50 }
            );
        }
    }
}
