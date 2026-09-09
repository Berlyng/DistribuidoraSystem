using Distribuidora.Application.Products.Abtractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Products;
using MediatR;

namespace Distribuidora.API.Products.GetById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductResponse>>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if(product is null)
            {
                return Result<ProductResponse>.Failure(ProductErrors.NotFound);
            }

            var presentations = product.Presentaciones.Select(presentation => new ProductPresentationResponse
           (
                presentation.Id,
                presentation.Name,
                presentation.ConversionFactor,
                presentation.RetailPrice,
                presentation.WholesalePrice,
                presentation.IsActive
            )).ToList();


            var response = new ProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.TaxType.ToString(),
                product.IsActive,
                presentations);

            return Result<ProductResponse>.Success(response);

        }
    }
}
