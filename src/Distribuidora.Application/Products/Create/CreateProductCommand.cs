using Distribuidora.Domain.Common;
using Distribuidora.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.Create
{
    public sealed record CreateProductCommand(
        string Name,
        string? Description,
        ProductTaxType TaxType,
        IReadOnlyCollection<CreateProductPresentationCommand> Presentations
    ) : IRequest<Result<Guid>>;

}
