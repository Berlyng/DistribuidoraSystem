using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Products
{
    public sealed class Product : BaseEntity
    {
        private readonly List<ProductPresentacion> _presentaciones = [];
        public Product()
        {
        }

        public Product(string name, string description, ProductTaxType taxType)
        {
            Name = name;
            Description = description;
            TaxType = taxType;
            IsActive = true;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public ProductTaxType TaxType { get; private set; }
        public bool IsActive { get; private set; }
        public IReadOnlyCollection<ProductPresentacion> Presentaciones => _presentaciones.AsReadOnly();


        public static Result<Product> Create(string name, string description, ProductTaxType taxType)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result<Product>.Failure(ProductErrors.NameRequired);
            if (name.Length > 150)
                return Result<Product>.Failure(ProductErrors.NameToolong);
            if (!string.IsNullOrWhiteSpace(description) && description.Length > 500)
                return Result<Product>.Failure(ProductErrors.DescriptionToolong);
            var product = new Product(name.Trim(), description?.Trim(), taxType);
            return Result<Product>.Success(product);
        }


        public Result AddPresentation(ProductPresentacion presentation)
        {
            var exists = _presentaciones.Any(x => x.Name.Equals(presentation.Name, StringComparison.OrdinalIgnoreCase));
            if (exists)
                return Result.Failure(ProductErrors.PresentationAlreadyExists);
            _presentaciones.Add(presentation);
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
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
