using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Products
{
    public sealed class ProductPresentacion : BaseEntity
    {
        public ProductPresentacion()
        {
        }

        public ProductPresentacion(string name, int conversionFactor, decimal retailPrice, decimal wholesalePrice)
        {
            Name = name;
            ConversionFactor = conversionFactor;
            RetailPrice = retailPrice;
            WholesalePrice = wholesalePrice;
            IsActive = true;
        }

        public string Name { get; private set; }
        public int ConversionFactor { get; private set; }
        public decimal RetailPrice { get; private set; }
        public decimal WholesalePrice { get; private set; }

        public bool IsActive { get; private set; }

        public static Result<ProductPresentacion> Create(string name, int conversionFactor, decimal retailPrice, decimal wholesalePrice)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result<ProductPresentacion>.Failure(ProductErrors.PresentationNameRequired);
            if (conversionFactor <= 0)
                return Result<ProductPresentacion>.Failure(ProductErrors.InvalidConversionFactor);
            if (retailPrice < 0)
                return Result<ProductPresentacion>.Failure(ProductErrors.InvalidPrice);
            if (wholesalePrice < 0)
                return Result<ProductPresentacion>.Failure(ProductErrors.InvalidPrice);
            var presentacion = new ProductPresentacion(name.Trim(), conversionFactor, retailPrice, wholesalePrice);
            return Result<ProductPresentacion>.Success(presentacion);
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
