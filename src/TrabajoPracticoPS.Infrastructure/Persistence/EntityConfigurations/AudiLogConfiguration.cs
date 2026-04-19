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
    public class AudiLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Action).IsRequired().HasMaxLength(100);
            builder.Property(a => a.EntityType).IsRequired().HasMaxLength(100);
            builder.Property(a => a.EntityId).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Details).HasColumnType("nvarchar(max)");

            builder.HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
