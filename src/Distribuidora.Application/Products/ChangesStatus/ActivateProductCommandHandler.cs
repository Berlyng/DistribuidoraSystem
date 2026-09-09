using Distribuidora.Application.Products.Abtractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.ChangesStatus
{
    public sealed class ActivateProductCommandHandler : IRequestHandler<ActivateProductCommand, Result>
    {
        private readonly IProductRepository _repository;

        public ActivateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
            if(product is null)
            {
                return Result.Failure(ProductErrors.NotFound);
            }

            product.Activate();

            await _repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
