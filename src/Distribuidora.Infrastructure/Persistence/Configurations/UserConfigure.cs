using Distribuidora.Domain.Users;
using Distribuidora.Domain.Users.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfigure : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(x => x.FirstName)
                .HasColumnName("Firstame")
                .HasMaxLength(100)
                .IsRequired();

                name.Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(100)
                .IsRequired();

            });

            builder.Property(x => x.Email)
                .HasConversion(email => email.Value, value => Email.FromPersistence(value))
                .HasColumnName("Email")
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasConversion(passwordHash => passwordHash.Value, value => PasswordHash.FromPersistence(value))
                .HasColumnName("PasswordHash")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Ignore(x => x.isDelete);
                


        }
    }
}
