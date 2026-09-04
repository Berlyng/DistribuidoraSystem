using Distribuidora.API.Customers.Create;
using Distribuidora.Application.Customers.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Distribuidora.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ISender _sender;
        public CustomerController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequests request, CancellationToken cancellationToken)
        {
            var command = new CreateCustomerCommand(request.Name, request.TaxId, request.PhoneNumber, request.Address, request.ContactName, request.CreditEnabled, request.CreditDays);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    code = result.Error.Code,
                    message = result.Error.Message,
                });
            }
            return Created($"/api/customers/{result.Value}", new { id = result.Value });
        }
    }
}
