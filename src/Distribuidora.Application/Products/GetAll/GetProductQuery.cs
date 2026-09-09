using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.GetAll
{
    public sealed record GetProductQuery(
        string? Search,
        bool? IsActive
        ) : IRequest<IReadOnlyList<ProductListItemResponse>>;
   
}
