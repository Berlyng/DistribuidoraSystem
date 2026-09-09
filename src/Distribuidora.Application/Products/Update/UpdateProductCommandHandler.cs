using Distribuidora.Application.Products.Abtractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.Update
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
            if(product is null)
            {
                return Result.Failure(ProductErrors.NotFound);
            }
            var productExist = await _repository.GetbyNameAsync(request.Name, cancellationToken);
            if(productExist is not null && productExist.Id != product.Id)
            {
                return Result.Failure(ProductErrors.NameAlreadyExists);

            }

            var updateResult = product.Update(request.Name, request.Description, request.TaxType);
            if (updateResult.IsFailure)
            {
                return updateResult;
            }

            await _repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
