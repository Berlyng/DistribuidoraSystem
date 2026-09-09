using Distribuidora.Application.Products.Abtractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.GetAll
{
    public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, IReadOnlyList<ProductListItemResponse>>
    {
        private IProductRepository _repository;

        public GetProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ProductListItemResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(request.Search, request.IsActive,  cancellationToken);

            return products
                .Select(p => new ProductListItemResponse
                (
                    p.Id,
                    p.Name,
                    p.Description,
                    p.TaxType.ToString(),
                    p.IsActive,
                    p.Presentaciones.Count
               )).ToList();

        }
    }
}
