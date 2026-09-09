using Distribuidora.Domain.Common;
using MediatR;

namespace Distribuidora.API.Products.GetById
{
    public sealed record GetProductByIdQuery(
        Guid Id) : IRequest<Result<ProductResponse>>;
 
}
