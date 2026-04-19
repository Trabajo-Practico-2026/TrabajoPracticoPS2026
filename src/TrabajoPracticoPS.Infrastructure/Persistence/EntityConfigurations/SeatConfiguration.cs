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
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("SEAT");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.RowIdentifier).IsRequired().HasMaxLength(10);
            builder.Property(s => s.Status).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Version).IsRequired().IsConcurrencyToken();

            builder.HasOne(s => s.Sector)
                .WithMany(s => s.Seats)
                .HasForeignKey(s => s.SectorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
