using Distribuidora.Domain.Customers;
using Distribuidora.Domain.Customers.Value_Object;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Infrastructure.Persistence.Configurations
{
    public sealed class CustomerConfigure : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasConversion(
                    name => name.Value,
                    value => CustomerName.Create(value).Value)
                .HasColumnName("Name")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.TaxId)
                .HasConversion(
                    taxId => taxId.Value,
                    value => TaxId.Create(value).Value)
                .HasColumnName("TaxId")
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(x => x.TaxId)
                .IsUnique();

            builder.Property(x => x.PhoneNumber)
                .HasConversion(
                    phoneNumber => phoneNumber.Value,
                    value => PhoneNumber.Create(value).Value)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasColumnName("Address")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.ContactName)
                .HasColumnName("ContactName")
                .HasMaxLength(150);

            builder.Property(x => x.CreditEnable)
                .IsRequired();

            builder.Property(x => x.CreditDays)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);
            builder.Ignore(x => x.isDelete);
                


        }
    }
}
