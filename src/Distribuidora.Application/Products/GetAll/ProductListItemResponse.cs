using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.GetAll
{
    public sealed record ProductListItemResponse(
        Guid Id,
        string Name,
        string? Description,
        string TaxType,
        bool IsActive,
        int PresentationCount);
  
}
