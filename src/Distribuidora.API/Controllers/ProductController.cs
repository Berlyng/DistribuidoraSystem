using Distribuidora.API.Products.Create;
using Distribuidora.Application.Products.Create;
using Distribuidora.Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Distribuidora.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<ProductTaxType>(
                 request.TaxType,
                 ignoreCase: true,
                 out var taxType))
            {
                return BadRequest(new
                {
                    code = "Product.InvalidTaxType",
                    message = "El tipo de impuesto especificado no es válido."
                });
            }

            var presentations = request.Presentations
                .Select(x => new CreateProductPresentationCommand(
                    x.Name,
                    x.ConversionFactor,
                    x.RetailPrice,
                    x.WholesalePrice)
                ).ToList();

            var command = new CreateProductCommand(request.Name, request.Description, taxType, presentations);
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    code = result.Error.Code,
                    message = result.Error.Message,
                });
            }


            return Created(
           $"/api/products/{result.Value}",
           new
           {
               id = result.Value
           });


        }
    }
}
