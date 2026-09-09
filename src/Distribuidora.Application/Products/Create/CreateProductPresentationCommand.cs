using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.Create
{
    public sealed record CreateProductPresentationCommand(
        string Name,
        int ConversionFactor,
        decimal RetailPrice,
        decimal WholesalePrice
    );
    
}
