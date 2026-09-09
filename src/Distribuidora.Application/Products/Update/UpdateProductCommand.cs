using Distribuidora.Domain.Common;
using Distribuidora.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.Update
{
    public sealed record UpdateProductCommand(
        Guid ProductId,
        string Name,
        string? Description,
        ProductTaxType TaxType) : IRequest<Result>;
   
}
