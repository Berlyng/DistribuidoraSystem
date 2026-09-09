using Distribuidora.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Infrastructure.Persistence.Configurations
{
    public sealed class ProductPresentationConfiguration : IEntityTypeConfiguration<ProductPresentacion>
    {
        public void Configure(EntityTypeBuilder<ProductPresentacion> builder)
        {
            builder.ToTable("ProductPresentations");
            builder.HasKey(pp => pp.Id);
            builder.Property(pp => pp.Id)
                .ValueGeneratedNever();

            builder.Property(pp => pp.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pp => pp.ConversionFactor)
                .IsRequired();

            builder.Property(pp => pp.RetailPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(pp => pp.WholesalePrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(pp => pp.IsActive)
                .IsRequired();

            builder.Property(pp => pp.CreatedAt)
                .IsRequired();

            builder.Property(pp => pp.UpdatedAt);
            builder.Property(pp => pp.DeletedAt);
            builder.Ignore(pp => pp.isDelete);

        }
    }
}
