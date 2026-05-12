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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();

            //Generador de 2 usuarios de ejemplo con password hash
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("password123");
            string hashedPassword2 = BCrypt.Net.BCrypt.HashPassword("password456");

            builder.HasData(
            new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PasswordHash = hashedPassword
            },
            new User
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                PasswordHash = hashedPassword2
            }
            );
        }
    }
}
