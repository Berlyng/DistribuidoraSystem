using Distribuidora.Application.Products.Abtractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.Create
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productExist = await _productRepository.ExistsByNameAsync(request.Name.Trim(), cancellationToken);
            if (productExist)
            {
                return Result<Guid>.Failure(ProductErrors.NameAlreadyExists);
            }


            if (request.Presentations is null || request.Presentations.Count == 0)
            {
                return Result<Guid>.Failure(ProductErrors.PresentationRequired);
            }

            var productResult = Product.Create(request.Name, request.Description, request.TaxType);

            if (productResult.IsFailure)
            {
                return Result<Guid>.Failure(productResult.Error);
            }

            var product = productResult.Value;


            foreach ( var item in request.Presentations)
            {
                var presentationResult = ProductPresentacion.Create(item.Name, item.ConversionFactor, item.RetailPrice, item.WholesalePrice);

                if(presentationResult.IsFailure)
                {
                    return Result<Guid>.Failure(presentationResult.Error);
                }

                var addResult = product.AddPresentation(presentationResult.Value);
                if (addResult.IsFailure)
                {
                    return Result<Guid>.Failure(addResult.Error);
                }

            }

            await _productRepository.AddAsync(product);

            await _productRepository.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(product.Id);

        }
    }
}
