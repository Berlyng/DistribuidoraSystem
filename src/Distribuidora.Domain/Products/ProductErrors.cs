using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Distribuidora.Domain.Products
{
    public static class ProductErrors
    {
        public static readonly Error NotFound = new("Product.NotFound", "El producto no fue encontrado.");
        public static readonly Error NameRequired = new("Product.NameRequired", "El nombre del producto es requerido.");
        public static readonly Error  NameToolong = new("Product.NameTooLong", "El nombre del producto es demasiado largo.");
        public static readonly Error DescriptionToolong = new("Product.DescriptionTooLong", "La descripción del producto es demasiado larga.");
        public static readonly Error PresentationNameRequired = new("Product.PresentationNameRequired", "El nombre de la presentación es requerido.");
        public static readonly Error InvalidConversionFactor = new("Product.InvalidConversionFactor", "El factor de conversión debe ser mayor a cero.");
        public static readonly Error InvalidPrice = new("Product.InvalidPrice", "El precio debe ser mayor o igual a cero.");
        public static readonly Error PresentationAlreadyExists = new("Product.PresentationAlreadyExists", "La presentación ya existe para este producto.");


    }
}
